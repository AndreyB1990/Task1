$('#news-panel-info').ready(function () {
    var info = $('#news-panel-info').data('info');
    $.getJSON(info.Url, function (data) {
        $.get(info.TemplatePath, function (template) {
            $.tmpl(template, data, { isAdmin: info.UserIsAdmin }).insertBefore($('#view-all-news'));
            sweetPages();
        });
    });
});