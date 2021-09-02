using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcIntro0.Extensions;
using MvcIntro0.Models;
using MvcIntro0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Controllers
{
    public class SummaryController : Controller
    {
        private readonly StoreContext _context;


        public SummaryController(StoreContext cntxt)
            => _context = cntxt;
        

        public IActionResult Index(string returnUrl)
            => View(new SummaryIndexViewModel { Summary = GetSummary(), ReturnUrl = returnUrl });


        public IActionResult Addition(int bikeId, string returnUrl)
        {
            var item = _context.Bikes.FirstOrDefault(item => item.BikeId == bikeId);

            if (item != null)
            {
                var summary = GetSummary();
                summary.Addition(item, 1);

                HttpContext.Session.Serialize("Summary", summary);
            }

            return RedirectToAction("Index", new { returnUrl });
        }


        public IActionResult Deletion(int bikeId, string returnUrl)
        {
            var item = _context.Bikes.FirstOrDefault(item => item.BikeId == bikeId);

            if (item != null)
            {
                var summary = GetSummary();
                summary.Deletion(item);

                HttpContext.Session.Serialize("Summary", summary);
            }

            return RedirectToAction("Index", new { returnUrl });
        }


        private Summary GetSummary()
        {
            var allItems = HttpContext.Session.Deserialize<Summary>("Summary");

            if (allItems == null)
            {
                allItems = new Summary();

                HttpContext.Session.Serialize("Summary", allItems);
            }

            return allItems;
        }
    }
}
