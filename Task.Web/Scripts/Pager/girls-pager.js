$(document).ready(function () {
    var info = $('#girls-items-info').data('info');
    var glInfo = $('#global-info').data('info');
    doPager(info.CountOfGirls, info.ItemsPerPage, glInfo.LinksPerPage, info.Page);
});