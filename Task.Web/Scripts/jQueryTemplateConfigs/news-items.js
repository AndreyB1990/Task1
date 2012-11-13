$('#news-items-info').ready(function () {
    var info = $('#news-items-info').data('info');

    $.getJSON('/News/List?page=' + info.Page, function(data) {
        $.get('/JQueryTemplates/News/NewsItemsTemplate.htm', function(template) {
            $.tmpl(template, data).insertBefore($(".pager"));
        });
    });
});