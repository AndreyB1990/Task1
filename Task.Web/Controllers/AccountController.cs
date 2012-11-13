using System.Web.Mvc;
using System.Web.Security;
using Task.Infrastructure;
using Task.Infrastructure.Models;
using Task.Infrastructure.UnitOfWork;
using Task.Services.Interfaces;
using Task.Web.Models.AccountModels;

namespace Task.Web.Controllers
{
    /// <summary>
    /// The controller which handles all the features associated with user accounts
    /// </summary>
    public class AccountController : Controller
    {

        private readonly IUserService _usersService;
        private readonly IRoleService _rolesService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="usersService"></param>
        /// <param name="rolesService"></param>
        /// <param name="unitOfWorkFactory"></param>
        public AccountController(IUserService usersService, IRoleService rolesService, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _usersService = usersService;
            _rolesService = rolesService;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Displays account panel
        /// </summary>
        /// <returns></returns>
        [ChildActionOnlyAttribute]
        public ActionResult AccountPanel()
        {
            return PartialView();
        }

        /// <summary>
        /// Displays the new admin registration page
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminSetup()
        {
            using (_unitOfWorkFactory.Create())
            {
                if (_rolesService.UserInRoleExists(Constants.ROLE_ADMIN))
                    return RedirectToAction("LogOn");
                return View(new RegisterModel());
            }
        }

        /// <summary>
        /// Processing a request for registration the new admin
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AdminSetup(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (var unit = _unitOfWorkFactory.Create())
                {
                    RegisterStatus createStatus;
                    _usersService.RegisterUser(model.UserName, model.Email, model.Password, out createStatus);
                    if (createStatus == RegisterStatus.Success)
                    {
                        _rolesService.CreateRole(Constants.ROLE_ADMIN);
                        _rolesService.AddUsersToRoles(new[] { model.UserName }, new[] { Constants.ROLE_ADMIN });
                        unit.Commit();
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }
            return View(model);
        }

        /// <summary>
        /// Displays the new user registration page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            using (_unitOfWorkFactory.Create())
            {
                if (!_rolesService.UserInRoleExists(Constants.ROLE_ADMIN))
                {
                    return RedirectToAction("AdminSetup");
                }
                return View(new RegisterModel());
            }
        }

        /// <summary>
        /// Processing a request for registration the new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (var unit = _unitOfWorkFactory.Create())
                {
                    RegisterStatus createStatus;
                    _usersService.RegisterUser(model.UserName, model.Email, model.Password, out createStatus);
                    if (createStatus == RegisterStatus.Success)
                    {
                        _rolesService.CreateRole(Constants.ROLE_USER);
                        _rolesService.AddUsersToRoles(new[] { model.UserName }, new[] { Constants.ROLE_USER });
                        unit.Commit();
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }
            return View(model);
        }

        /// <summary>
        /// Displays a partial login form on the website
        /// </summary>
        /// <returns></returns>
        [ChildActionOnlyAttribute]
        public ActionResult LogOnPartial()
        {
            if (User.Identity.IsAuthenticated)
                return new EmptyResult();
            return PartialView(new LogOnModel());
        }

        /// <summary>
        /// Displays a simple login form on the website
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOn()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View(new LogOnModel());
        }

        /// <summary>
        /// Processes the user's request to visit the site
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (_unitOfWorkFactory.Create())
                {
                    if (_usersService.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Closes the current user's authorization
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Checking certain username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public JsonResult ValidateName(string username)
        {
            using (_unitOfWorkFactory.Create())
            {
                return _usersService.CheckUserLogin(username)
                           ? Json(true, JsonRequestBehavior.AllowGet)
                           : Json("This username is already registered.", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Checking certain email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonResult ValidateEmail(string email)
        {
            using (_unitOfWorkFactory.Create())
            {
                return _usersService.CheckUserEmail(email)
                           ? Json(true, JsonRequestBehavior.AllowGet)
                           : Json("User with that mailbox is already registered.", JsonRequestBehavior.AllowGet);
            }
        }

        #region Status Codes
        private static string ErrorCodeToString(RegisterStatus createStatus)
        {
            switch (createStatus)
            {
                case RegisterStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case RegisterStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case RegisterStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case RegisterStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case RegisterStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case RegisterStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case RegisterStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
