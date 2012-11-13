$('#news-panel-info').ready(function () {
    var info = $('#news-panel-info').data('info');

    $.getJSON('/News/LatestNews?page=' + info.Page, function(data) {
        $.get('/JQueryTemplates/News/NewsPanelItemsTemplate.htm', function(template) {
            $.tmpl(template, data, { isAdmin: info.UserIsAdmin }).insertBefore($('#view-all-news'));
            sweetPages();
        });
    });
});