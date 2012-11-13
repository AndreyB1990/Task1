jQuery.fn.pagination = function (maxentries, opts) {
    opts = jQuery.extend({
        items_per_page: 10,
        num_display_entries: 10,
        current_page: 0,
        link_to: "#",
        ellipse_text: "...",
        update_content: "#content",
        callback: function () { return false; }
    }, opts || {});

    return this.each(function () {
        function numPages() {
            return Math.ceil(maxentries / opts.items_per_page);
        }

        function pageSelected(pageId) {
            current_page = pageId;
            drawLinks();
        }

        function drawLinks() {
            panel.empty();
            var dots1 = false;
            var dots2 = false;
            var np = numPages();
            var getClickHandler = function (pageId) {
                return function () { return pageSelected(pageId); };
            };
            var appendItem = function (page) {
                page = page <= 0 ? 1 : (page <= np ? page : np);
                var lnk;
                if (page == current_page) {
                    lnk = jQuery("<span class='pager-number'>" + page + "</span>");
                } else {
                    var pageLink = jQuery("<a>" + page + "</a>")
                            .bind("click", getClickHandler(page))
                            .attr('href', String.Format('{0}page={1}', opts.link_to, page))
                            .attr('data-ajax', 'true')
                            .attr('data-ajax-mode', 'replace')
                            .attr('data-ajax-update', opts.update_content);
                    lnk = $("<span class='pager-number'>").append(pageLink);
                }
                panel.append(lnk);
            };
            for (var i = 1; i <= np; i++) {
                if (i == 1 || i == np || (i <= current_page && i >= (current_page - opts.num_display_entries))
                        || (i >= current_page && i <= (current_page + opts.num_display_entries)))
                    appendItem(i);
                else {
                    if (i < current_page) {
                        if (!dots1) {
                            jQuery("<span>" + opts.ellipse_text + "</span>").appendTo(panel);
                            dots1 = true;
                        }
                    } else {
                        if (!dots2) {
                            jQuery("<span>" + opts.ellipse_text + "</span>").appendTo(panel);
                            dots2 = true;
                        }
                    }
                }
            }
        }
        current_page = parseInt(opts.current_page, 10);
        maxentries = (!maxentries || maxentries < 0) ? 1 : parseInt(maxentries, 10);
        opts.num_display_entries = (!opts.num_display_entries || opts.num_display_entries < 0) ? 1 : parseInt(opts.num_display_entries, 10);
        if (maxentries <= opts.items_per_page)
            return false;
        opts.items_per_page = (!opts.items_per_page || opts.items_per_page < 0) ? 1 : parseInt(opts.items_per_page, 10);
        var panel = jQuery(this);
        drawLinks();
        return false;
    });
};