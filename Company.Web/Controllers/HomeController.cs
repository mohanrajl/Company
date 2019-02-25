using Company.Provider;
using Company.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Company.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                FormsAuthentication.SignOut();
            }

            return View();
        }
        
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Secure");
            }

            if (TempData.Count > 0 && TempData["NotValid"] != null)
            {
                ViewBag.NotValid = TempData["NotValid"].ToString();
            }

            if (TempData.Count > 0 && TempData["ErrorMessage"] != null)
            {
                ViewBag.NotValid = TempData["ErrorMessage"].ToString();
            }

            return View("~/Views/Login.cshtml");
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {                
                var userList = new UserProvider().GetUsers().ToList();                
                var user = userList.Where(item => item.Name.Equals(loginViewModel.UserName.Trim()) && item.Password.Equals(loginViewModel.Password.Trim()) && item.Active == true).FirstOrDefault();
                if (user != null)
                {                    
                    FormsAuthentication.SetAuthCookie(user.Name, true);
                    Session["UserId"] = user.Id;
                    if (user.Admin)
                    {
                        Session["IsAdmin"] = "Yes";
                    }

                    return RedirectToAction("Home");
                }
                else
                {
                    TempData["NotValid"] = "Invalid username or password";
                }
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Login");
        }
        
        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["IsAdmin"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult Home()
        {
            ViewBag.Message = "Home";

            return View("~/Views/Home.cshtml");
        }
    }
}
