using System.Linq;
using System.Web.Mvc;
using Task.DALModels;
using Task.Infrastructure;
using Task.Infrastructure.UnitOfWork;
using Task.Services.Interfaces;

namespace Task.Web.Controllers
{
    public class GirlsController : Controller
    {
        private readonly IGirlService _girlsService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        /// <summary>
        /// Base controller
        /// </summary>
        /// <param name="girlsService"></param>
        /// <param name="unitOfWorkFactory"></param>
        public GirlsController(IGirlService girlsService, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _girlsService = girlsService;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Returns JSON object with news items on current page and indicates are these girl items beautiful  
        /// </summary>
        /// <param name="page"></param>
        /// <param name="isBeautiful"></param>
        /// <returns></returns>
        public ActionResult List(int? page, bool? isBeautiful)
        {
            using (_unitOfWorkFactory.Create())
            {
                if (page == null || page < 1) page = 1;
                if (isBeautiful == null) isBeautiful = false;
                var skip = (int) ((page - 1) * Constants.GIRLS_PAGER_LINKS_PER_PAGE);
                const int take = Constants.GIRLS_PAGER_LINKS_PER_PAGE;
                var girls = (bool)(isBeautiful) ? _girlsService.GetBeautifulGirls().Skip(skip).Take(take) : _girlsService.Get(skip, take);
                return Json(girls.ConvertToGirlModels(), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Displays a list of girl items on current page and indicates are these girl items beautiful
        /// </summary>
        /// <param name="page"></param>
        /// <param name="isBeautiful"></param>
        /// <returns></returns>
        public ActionResult Index(int? page, bool? isBeautiful)
        {
            if (page == null || page < 1) page = 1;
            if (isBeautiful == null) isBeautiful = false;
            ViewBag.userIsAdmin = IsUserInRoleMethod.IsUserInRole(User.Identity.Name, Constants.ROLE_ADMIN);
            ViewBag.IsBeautiful = isBeautiful;
            ViewBag.Page = (int)page;
            ViewBag.CountOfGirls = (bool)(isBeautiful) ? _girlsService.GetBeautifulGirls().Count() : _girlsService.GetAll().Count();
            return View();
        }

        /// <summary>
        /// Displays the new girl object create page
        /// </summary>
        /// <returns></returns>
        [AuthorizeOwner(Roles = Constants.ROLE_ADMIN)]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Processing a request for creating the new girl object
        /// </summary>
        /// <param name="girl"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeOwner(Roles = Constants.ROLE_ADMIN)]
        public ActionResult Create(Girl girl)
        {
            if (ModelState.IsValid)
            {
                using (var unit = _unitOfWorkFactory.Create())
                {
                    _girlsService.Add(girl);
                    unit.Commit();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        /// <summary>
        /// Displays the girl object edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AuthorizeOwner(Roles = Constants.ROLE_ADMIN)]
        public ActionResult Edit(int id)
        {
            using (_unitOfWorkFactory.Create())
            {
                var obj = _girlsService.Get(id);
                if (obj == null) return RedirectToAction("NotFound", "Error");
                return View(obj);
            }
        }

        /// <summary>
        /// Processing a request for editing the girl object
        /// </summary>
        /// <param name="girl"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeOwner(Roles = Constants.ROLE_ADMIN)]
        public ActionResult Edit(Girl girl)
        {
            if (ModelState.IsValid)
            {
                using (var unit = _unitOfWorkFactory.Create())
                {
                    _girlsService.Update(girl);
                    unit.Commit();
                    return RedirectToAction("Index");
                }
            }
            return View(girl);
        }

        /// <summary>
        /// Deletes the girl object using ajax mode
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
                    var obj = _girlsService.Get(id);
                    if (obj == null) return new JsonResult { Data = new { @url = Url.Action("NotFound", "Error") } };
                    _girlsService.Delete(obj);
                    unit.Commit();
                    return RedirectToAction("Index");
                }
            }
            return new JsonResult { Data = new { @url = Url.Action("Index") } };
           
        }
    }
}
