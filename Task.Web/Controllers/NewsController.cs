using System.Linq;
using System.Web.Mvc;
using Task.DALModels;
using Task.Infrastructure;
using Task.Infrastructure.UnitOfWork;
using Task.Services.Interfaces;

namespace Task.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        /// <summary>
        /// Base controller
        /// </summary>
        /// <param name="newsService"></param>
        /// <param name="unitOfWorkFactory"></param>
        public NewsController(INewsService newsService, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _newsService = newsService;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Returns JSON object with news items on current page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public JsonResult List(int? page)
        {
            using (_unitOfWorkFactory.Create())
            {
                if (page == null || page < 1) page = 1;
                var skip = (int)(page - 1) * Constants.NEWS_PAGER_LINKS_PER_PAGE;
                const int take = Constants.NEWS_PAGER_LINKS_PER_PAGE;
                var news = _newsService.Get(skip, take);
                ViewBag.CountOfNews = _newsService.GetAll().Count();
                ViewBag.Page = (int)page;
                return Json(news, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Returns JSON object with latest news
        /// </summary>
        /// <returns></returns>
        public JsonResult LatestNews()
        {
            using (_unitOfWorkFactory.Create())
            {
                var news = _newsService.GetLatestNews();
                return Json(news, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Returns JSON object with news item with current ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Item(int id)
        {
            using (_unitOfWorkFactory.Create())
            {
                var news = _newsService.Get(id);
                return Json(news, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Displays a list of news items on current page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int? page)
        {
            using (_unitOfWorkFactory.Create())
            {
                if (page == null || page < 1) page = 1;
                ViewBag.CountOfNews = _newsService.GetAll().Count();
                ViewBag.Page = (int) page;
                return View();
            }
        }

        /// <summary>
        /// Displays partial view with latest news objects
        /// </summary>
        /// <returns></returns>
        [ChildActionOnlyAttribute]
        public ActionResult NewsPanel()
        {
            using (_unitOfWorkFactory.Create())
            {
                ViewBag.userIsAdmin = IsUserInRoleMethod.IsUserInRole(User.Identity.Name, Constants.ROLE_ADMIN);
                return PartialView();
            }
        }

        /// <summary>
        /// Displays the news object details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            using (_unitOfWorkFactory.Create())
            {
                ViewBag.userIsAdmin = IsUserInRoleMethod.IsUserInRole(User.Identity.Name, Constants.ROLE_ADMIN);
                return View(id);
            }
        }

        /// <summary>
        /// Displays the new news object create page
        /// </summary>
        /// <returns></returns>
        [AuthorizeOwner(Roles = Constants.ROLE_ADMIN)]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Processing a request for creating the new news object
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeOwner(Roles = Constants.ROLE_ADMIN)]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                using (var unit = _unitOfWorkFactory.Create())
                {
                    _newsService.Add(news);
                    unit.Commit();
                    return RedirectToAction("Details", new { id = news.Id });
                }
            }
            return View();
        }

        /// <summary>
        /// Displays the news object edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizeOwner(Roles = Constants.ROLE_ADMIN)]
        public ActionResult Edit(int id)
        {
            using (_unitOfWorkFactory.Create())
            {
                var obj = _newsService.Get(id);
                if (obj == null) return RedirectToAction("NotFound", "Error");
                return View(obj);
            }
        }

        /// <summary>
        /// Processing a request for editing the news object
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeOwner(Roles = Constants.ROLE_ADMIN)]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                using (var unit = _unitOfWorkFactory.Create())
                {
                    _newsService.Update(news);
                    unit.Commit();
                    return RedirectToAction("Details", new { id = news.Id });
                }
            }
            return View(news);
        }

        /// <summary>
        /// Deletes the news object using ajax mode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeOwner(Roles = Constants.ROLE_ADMIN)]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                using (var unit = _unitOfWorkFactory.Create())
                {
                    var obj = _newsService.Get(id);
                    if (obj == null) return new JsonResult { Data = new { @url = Url.Action("Index") } };
                    _newsService.Delete(obj);
                    unit.Commit();
                    ViewBag.userIsAdmin = IsUserInRoleMethod.IsUserInRole(User.Identity.Name, Constants.ROLE_ADMIN);
                    return PartialView("NewsPanel", _newsService.GetLatestNews());
                }
            }
            return new JsonResult { Data = new { @url = Url.Action("Index") } };
        }
    }
}
