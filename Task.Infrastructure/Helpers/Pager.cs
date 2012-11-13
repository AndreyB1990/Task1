using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Task.Infrastructure.Helpers
{
    public class Pager
    {
        /// <summary>
        /// A list of links to pages
        /// </summary>
        private readonly List<string> _links;

        /// <summary>
        /// A list of links to pages
        /// </summary>
        public List<string> Links { get { return _links; } }

        /// <summary>
        /// The number of objects on a page (default - 0)
        /// </summary>
        protected int PerPage = 0;

        /// <summary>
        /// The number of displayed pages before ellipsis
        /// </summary>
        protected int Visible = Constants.PAGER_NUMBER_OF_VISIBLE_LINKS;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="linksPerPage">the number of objects in one page</param>
        /// <param name="currentPage">the current page number</param>
        /// <param name="totalCount">the number of objects in all (on all pages)</param>
        /// <param name="queryString">full string GET-parameters</param>
        public Pager(int linksPerPage, int currentPage, int totalCount, System.Collections.Specialized.NameValueCollection queryString)
        {
            PerPage = linksPerPage;
            if (currentPage < 1) currentPage = 1;
            _links = GetLinks(totalCount, currentPage, PerPage, Visible, queryString);
        }

        /// <summary>
        /// Creating a list of links on the page
        /// </summary>
        /// <param name="countItems">the number of objects in all (on all pages)</param>
        /// <param name="currentPage">the current page number</param>
        /// <param name="itemsPerPage">the number of objects in one page</param>
        /// <param name="linksVisible">the number of visible links</param>
        /// <param name="query">full string GET-parameters</param>
        /// <returns></returns>
        protected List<string> GetLinks(int countItems, int currentPage, int itemsPerPage, int linksVisible, System.Collections.Specialized.NameValueCollection query)
        {
            string strQueryString = query.Keys.Cast<string>().Where(key => key != "page" && key != "X-Requested-With").Aggregate("?", (current, key) => string.Format("{0}{1}={2}&", current, key, query[key]));
            var countPages = (int)Math.Ceiling(countItems / (double)itemsPerPage);
            var result = new List<string>();
            bool bThreeDots1 = false;
            bool bThreeDots2 = false;
            int linksVisibleHead = linksVisible;
            if (linksVisible >= (currentPage - linksVisible)) linksVisibleHead = linksVisible * 3 + 1;
            int linksVisibleTail = linksVisible;
            if ((currentPage + linksVisible) >= (countPages - linksVisible)) linksVisibleTail = linksVisible * 3 + 1;

            for (int i = 1; i <= countPages; i++)
            {
                if (i <= linksVisibleHead
                    || i > (countPages - linksVisibleTail)
                    || (i <= currentPage && i >= (currentPage - linksVisible))
                    || (i >= currentPage && i <= (currentPage + linksVisible)))
                {
                    result.Add(i == currentPage
                                   ? i.ToString(CultureInfo.InvariantCulture)
                                   : string.Format(
                                       "<a href='{0}page={1}' data-ajax-update=\"#content\" data-ajax-mode=\"replace\" data-ajax=\"true\">{2}</a>", strQueryString, i, i));
                }
                else
                {
                    if (i < currentPage)
                    {
                        if (!bThreeDots1)
                        {
                            result.Add("...");
                            bThreeDots1 = true;
                        }
                    }
                    else
                    {
                        if (!bThreeDots2)
                        {
                            result.Add("...");
                            bThreeDots2 = true;
                        }
                    }
                }
            }

            return result;
        }
    }
}