using Company.Models;
using Company.Provider;
using Company.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Company.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }
    }
}