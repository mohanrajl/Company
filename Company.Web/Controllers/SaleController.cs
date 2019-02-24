using Company.Models;
using Company.Provider;
using Company.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Company.Web.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                var saleList = new SaleProvider().GetSales(int.Parse(Session["UserId"].ToString()));

                if (saleList != null && saleList.Count > 0)
                {
                    var saleViewModelList = new List<SaleViewModel>();

                    foreach (var saleItem in saleList)
                    {
                        var saleViewModel = new SaleViewModel
                        {
                            Id = saleItem.Id,
                            UserId = saleItem.UserId,
                            Date = saleItem.Date,
                            Description = saleItem.Description,
                            VatApplied = saleItem.VatApplied,
                            Active = saleItem.Active,
                            NetAmount = saleItem.NetAmount
                        };

                        saleViewModelList.Add(saleViewModel);
                    }

                    return View(saleViewModelList.AsEnumerable());
                }
            }

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SaleViewModel saleViewModel)
        {
            if (saleViewModel != null)
            {                       
                var createSaleModel = new Sale()
                {
                    UserId = int.Parse(Session["UserId"].ToString()),
                    Date = saleViewModel.Date,
                    Description = saleViewModel.Description,
                    NetAmount = saleViewModel.NetAmount,
                    VatApplied = saleViewModel.VatApplied,
                    Active = true
                };

                var saleId = new SaleProvider().CreateSale(createSaleModel);
                if (!string.IsNullOrEmpty(saleId))
                {
                    return RedirectToAction("Index");
                }                                    
            }

            return View();
        }
    }
}