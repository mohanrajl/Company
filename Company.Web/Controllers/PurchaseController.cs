using Company.Models;
using Company.Provider;
using Company.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Company.Web.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                var purchaseList = new PurchaseProvider().GetPurchases(int.Parse(Session["UserId"].ToString()));

                if (purchaseList != null && purchaseList.Count > 0)
                {
                    var purchaseViewModelList = new List<PurchaseViewModel>();

                    foreach (var purchaseItem in purchaseList)
                    {
                        var purchaseViewModel = new PurchaseViewModel
                        {
                            Id = purchaseItem.Id,
                            UserId = purchaseItem.UserId,
                            Date = purchaseItem.Date,
                            Description = purchaseItem.Description,
                            VatApplied = purchaseItem.VatApplied,
                            Active = purchaseItem.Active,
                            NetAmount = purchaseItem.NetAmount
                        };

                        purchaseViewModelList.Add(purchaseViewModel);
                    }

                    return View(purchaseViewModelList.AsEnumerable());
                }
            }

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PurchaseViewModel purchaseViewModel)
        {
            if (purchaseViewModel != null)
            {                       
                var createPurchaseModel = new Purchase()
                {
                    UserId = int.Parse(Session["UserId"].ToString()),
                    Date = purchaseViewModel.Date,
                    Description = purchaseViewModel.Description,
                    NetAmount = purchaseViewModel.NetAmount,
                    VatApplied = purchaseViewModel.VatApplied,
                    Active = true
                };

                var purchaseId = new PurchaseProvider().CreatePurchase(createPurchaseModel);
                if (!string.IsNullOrEmpty(purchaseId))
                {
                    return RedirectToAction("Index");
                }                                    
            }

            return View();
        }
    }
}