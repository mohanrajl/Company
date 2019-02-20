using System.Configuration;
using System.Net.Mail;
using System.Web.Mvc;

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
            var toAddress = ConfigurationManager.AppSettings["toAddress"];
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailMessage.From = new MailAddress(email);
            mailMessage.To.Add(new MailAddress(toAddress));
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;
            smtp.Send(mailMessage);
        }
    }
}
