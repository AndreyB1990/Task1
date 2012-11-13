(function (st) {
    st.format = function () {
        if (arguments.length == 0) return this;
        var placeholder = /\{(\d+)(?:,([-+]?\d+))?(?:\:([^(^}]+)(?:\(((?:\\\)|[^)])+)\)){0,1}){0,1}\}/g;
        var args = arguments;
        return this.replace(placeholder, function (m, num, len, f, params) {
            m = args[Number(num)];
            f = formatters[f];
            return fl(f == null ? m : f(m, pp((params || '').replace(/\\\)/g, ')').replace(/\\,/g, '\0').split(','), args)), len);
        });
    };
    String.Format = function (format) { return arguments.length <= 1 ? format : st.format.apply(format, Array.prototype.slice.call(arguments, 1)); };
    st.format.add = function (name, func, replace) {
        if (formatters[name] != null && replace != true) throw 'Format ' + name + ' exist, use replace=true for replace';
        formatters[name] = func;
    };
    String.Format.init = st.format.init = function (param) {
        var f;
        for (var n in param) {
            f = formatters[n];
            if (f != null && f.init != null) f.init(param[n]);
        }
    };
    st.format.get = function (name) { return formatters[name]; };
    var paramph = /^\{(\d+)\}$/;
    var formatters = {};
    var sp = '    ';

    function pp(params, args) {
        var r;
        for (var i = 0; i < params.length; i++) {
            if ((r = paramph.exec(params[i])) != null) params[i] = args[Number(r[1])];
            else params[i] = params[i].replace(/\0/g, ',');
        }
        return params;
    }

    function fl(s, len) {
        len = Number(len);
        if (isNaN(len)) return s;
        s = '' + s;
        var nl = Math.abs(len) - s.length;
        if (nl <= 0) return s;
        while (sp.length < nl) sp += sp;
        return len < 0 ? (s + sp.substring(0, nl)) : (sp.substring(0, nl) + s);
    }

    st.format.add('arr', function arr(va, params) {
        if (va == null) return 'null';
        var v = [];
        var j = params.shift() || '';
        var f = formatters[params.shift()];
        if (f == null) v = va;
        else for (var i = 0; i < va.length; i++) v.push(f(va[i], params));
        return v.join(j);
    });
})(String.prototype);