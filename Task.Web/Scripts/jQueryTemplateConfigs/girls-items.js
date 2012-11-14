$('#girls-items-info').ready(function () {
    var info = $('#girls-items-info').data('info');

    $.getJSON(info.Url, function (data) {
        $.get(info.TemplatePath, function (template) {
            $.tmpl(template, data, { isAdmin: info.UserIsAdmin }).insertAfter($('#table-girls-tr-title'));
        });
    });
});