using System.Net;
using System.Web.Mvc;

namespace Task.Web.Controllers
{
    /// <summary>
    /// The controller which handles all erros on the site
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Displays the error page with InternalServer error
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View("Error");
        }

        /// <summary>
        /// Displays the error page with NotFound error
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("NotFound");
        }

        /// <summary>
        /// Displays the error page with Forbidden error
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return View("Forbidden");
        }

    }
}
