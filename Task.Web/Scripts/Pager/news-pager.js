$(document).ready(function () {
    var info = $('#news-items-info').data('info');
    var glInfo = $('#global-info').data('info');
    doPager(info.CountOfNews, info.ItemsPerPage, glInfo.LinksPerPage, info.Page);
});