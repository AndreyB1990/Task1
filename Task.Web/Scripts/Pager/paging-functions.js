var historyedited = false;

$(document).ready(function () {
    window.addEventListener("popstate", function () {
        if (historyedited) {
            var url = location.pathname + location.search;
            $('#content').empty().load(url, function () {
            });
        }
    }, false);
});

function getPathWithoutCurrentParam(param) {
    var linkTo = location.pathname;
    if (location.search.split(String.Format('?{0}=', param))[0] != location.search) {
        linkTo += location.search.split(String.Format('{0}=', param))[0];
    }
    else if (location.search.split(String.Format('&{0}=', param))[0] != location.search) {
        linkTo += location.search.split(String.Format('{0}=', param))[0];
    }
    else if (location.search.substring(1) != '') {
        linkTo += location.search + '&';
    } else {
        linkTo += '?';
    }
    return linkTo;
}

function doPager(totalCount, itemsPerPage, numDisplayEntries, currentPage) {
    $('.pager').pagination(totalCount, {
        items_per_page: itemsPerPage,
        num_display_entries: numDisplayEntries,
        link_to: getPathWithoutCurrentParam('page'),
        current_page: currentPage
    });
    $('.pager-number a').click(function (e) {
        e.preventDefault();
        window.history.pushState(null, null, $(this).attr('href'));
        historyedited = true;
    });
}