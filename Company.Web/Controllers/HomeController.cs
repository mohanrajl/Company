using Company.Provider;
using Company.Web.Models;
using System.Configuration;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;

namespace Company.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Feedback()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Elements()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult OurHistory()
        {
            ViewBag.Message = "Our History.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Upload(string name, string email, string subject, string message)
        {
            ViewBag.Message = "Your contact page.";
            sendEmail(name, email, subject, message);
            return View("Contact");
        }

        private void sendEmail(string name, string email, string subject, string message)
        {
            var toEmailAddress = ConfigurationManager.AppSettings["toEmailAddress"];
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailMessage.From = new MailAddress(email);
            mailMessage.To.Add(new MailAddress(toEmailAddress));
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;
            smtp.Send(mailMessage);
        }
        
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Secure");
            }

            ViewBag.Message = "login";

            return View("~/Views/Login.cshtml");
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            var user = new UserProvider().GetUsers().Where(item => item.Name.Equals(loginViewModel.UserName.Trim()) && item.Password.Equals(loginViewModel.Password.Trim()) && item.Active == true).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Name, true);
                Session["UserId"] = user.Id;
                return RedirectToAction("Home");
            }

            ViewBag.Message = "login";
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult Logout()
        {
            Session["UserId"] = null;
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
