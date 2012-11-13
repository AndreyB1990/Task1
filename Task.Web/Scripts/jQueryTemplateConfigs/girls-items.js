$('#girls-items-info').ready(function () {
    var info = $('#girls-items-info').data('info');
    
    $.getJSON('/Girls/List?isBeautiful=' + info.IsBeautiful + '&page=' + info.Page, function (data) {
        $.get('/JQueryTemplates/Girls/GirlsItemsTemplate.htm', function (template) {
            $.tmpl(template, data, { isAdmin: info.UserIsAdmin }).insertAfter($('#table-girls-tr-title'));
        });
    });
});