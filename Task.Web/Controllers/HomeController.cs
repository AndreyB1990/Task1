using System.Web.Mvc;

namespace Task.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Displays the home page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the page "About the site"
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }
    }
}
