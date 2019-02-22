using Company.Provider;
using Company.Web.Models;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;

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
            var user = new UserProvider().GetUsers().Where(item => item.Name.Equals(login.UserName.Trim()) && item.Password.Equals(login.Password.Trim()) && item.Active == true).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Name, true);
                Session["UserId"] = user.Id;
                return RedirectToAction("Home", "Secure");
            }

            ViewBag.Message = "login";
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
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
