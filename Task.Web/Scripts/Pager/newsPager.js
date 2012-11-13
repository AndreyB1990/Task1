var info = $('#news-items-info').data('info');
var glInfo = $('#global-info').data('info');

$(document).ready(function () {
    doPager(info.CountOfNews, info.ItemsPerPage, glInfo.LinksPerPage, info.Page);
});