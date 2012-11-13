//Adds sweetPages to current element
(function ($) {
    $.fn.sweetPages = function (opts) {
        if (!opts) opts = {};

        var resultsPerPage = Math.floor(opts.perPage) || 3;
        var ul = this;
        var li = ul.find('li');

        li.each(function () {
            var el = $(this);
            el.data('height', el.outerHeight(true));
        });
        var pagesNumber = Math.ceil(li.length / resultsPerPage);
        var outer = $('<div class="outer-center">');
        var inner = $('<div class="inner-center">');
        outer.append(inner);
        var swControls = $('<div class="swControls">');
        inner.append(swControls);
        for (var i = 0; i < pagesNumber; i++) {
            li.slice(i * resultsPerPage, (i + 1) * resultsPerPage).wrapAll('<div class="swPage" />');
            if (pagesNumber >= 2)
                swControls.append('<a href="" class="swShowPage">' + (i + 1) + '</a>');
        }
        var maxHeight = 0;
        var totalWidth = 0;

        var swPage = ul.find('.swPage');
        swPage.each(function () {
            var elem = $(this);
            var tmpHeight = 0;
            elem.find('li').each(function () { tmpHeight += $(this).data('height'); });
            if (tmpHeight > maxHeight)
                maxHeight = tmpHeight;

            totalWidth += elem.outerWidth();

            elem.css('float', 'left').width(ul.width());
        });

        swPage.wrapAll('<div class="swSlider" />');

        var swSlider = ul.find('.swSlider');
        swSlider.append('<div class="clear" />').width(totalWidth);
        ul.append(outer);

        var hyperLinks = ul.find('a.swShowPage');

        hyperLinks.click(function (e) {
            $(this).addClass('active').siblings().removeClass('active');
            swSlider.stop().animate({ 'margin-left': -(parseInt($(this).text()) - 1) * ul.width() }, 'slow');
            e.preventDefault();
        });

        hyperLinks.eq(0).addClass('active');
        return this;

    };
})(jQuery);