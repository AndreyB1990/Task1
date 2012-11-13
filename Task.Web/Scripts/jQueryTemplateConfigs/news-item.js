$('#news-item-info').ready(function () {
    var info = $('#news-item-info').data('info');

    $.getJSON('/News/Item/' + info.Id, function(data) {
        $.get('/JQueryTemplates/News/NewsItemTemplate.htm', function(template) {
            $.tmpl(template, data,
                { isAdmin: info.UserIsAdmin }).appendTo($('#news-items-container'));
        });
    });
});