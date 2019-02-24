using Company.Web.Models;
using System.Configuration;
using System.Net.Mail;
using System.Web.Mvc;

namespace Company.Web.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LegalNotice()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(SendEmailViewModel sendEmailViewModel)
        {
            ViewBag.Message = "Your contact page.";
            Send(sendEmailViewModel);
            return RedirectToAction("Index");
        }

        private void Send(SendEmailViewModel sendEmailViewModel)
        {
            var toEmailAddress = ConfigurationManager.AppSettings["toEmailAddress"];
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailMessage.From = new MailAddress(sendEmailViewModel.Email);
            mailMessage.To.Add(new MailAddress(toEmailAddress));
            mailMessage.Subject = sendEmailViewModel.Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sendEmailViewModel.Message;
            smtp.Send(mailMessage);
        }
    }
}
