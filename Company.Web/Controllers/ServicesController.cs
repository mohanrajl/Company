using System.Web.Mvc;

namespace Company.Web.Controllers
{
    public class ServicesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaxConsultancy()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Accounting()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}
