using System.Web.Mvc;
using System.Web.Security;
using Company.Web.Models;

namespace Company.Web.Controllers
{
    [Authorize]
    public class SecureController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Message = "login";

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (login.UserName == "test")
            {
                FormsAuthentication.SetAuthCookie(login.UserName, true);
                return RedirectToAction("Home", "Secure");
            }

            ViewBag.Message = "login";

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Secure");
        }

        public ActionResult Home()
        {
            ViewBag.Message = "Welcome User " + User.Identity.Name;

            return View();
        }
    }
}
