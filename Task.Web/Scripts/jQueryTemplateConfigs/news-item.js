$('#news-item-info').ready(function () {
    var info = $('#news-item-info').data('info');

    $.getJSON(info.Url, function (data) {
        $.get(info.TemplatePath, function (template) {
            $.tmpl(template, data,
                { isAdmin: info.UserIsAdmin }).appendTo($('#news-items-container'));
        });
    });
});