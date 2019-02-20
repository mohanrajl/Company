using System.Web.Mvc;

namespace Company.Web.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "About.";

            return View();
        }

        public ActionResult History()
        {
            ViewBag.Message = "History.";

            return View();
        }

        public ActionResult Partners()
        {
            ViewBag.Message = "Partners.";

            return View();
        }

        public ActionResult Vacancies()
        {
            ViewBag.Message = "Vacancies.";

            return View();
        }
    }
}
