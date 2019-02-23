using System.Web.Mvc;

namespace Company.Web.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult History()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Partners()
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
    }
}
