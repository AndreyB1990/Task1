$('#news-items-info').ready(function () {
    var info = $('#news-items-info').data('info');

    $.getJSON(info.Url, function (data) {
        $.get(info.TemplatePath, function (template) {
            $.tmpl(template, data).insertBefore($(".pager"));
        });
    });
});