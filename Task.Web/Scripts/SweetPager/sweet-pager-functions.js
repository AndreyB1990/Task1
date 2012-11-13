//Adds sweetPages to #news-list item
function sweetPages() {
    if ($('#news-list')[0] !== undefined) {
        $('#news-list').sweetPager({ perPage: countOfNewsOnPage() });
        var controls = $('#news-list .outer-center').detach();
        controls.appendTo('#news-main');
    }
};

//Calculates number of news items on #news panel
function countOfNewsOnPage() {
    return ($('#news').outerHeight(true) - $('#news h2').outerHeight(true)
                - $('#news-list a').outerHeight(true)) / ($('#news-list li').outerHeight(true));
}