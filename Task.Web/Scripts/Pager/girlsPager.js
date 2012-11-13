var info = $('#girls-items-info').data('info');
var glInfo = $('#global-info').data('info');

$(document).ready(function () {
    doPager(info.CountOfGirls, info.ItemsPerPage, glInfo.LinksPerPage, info.Page);
});