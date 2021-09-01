using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcIntro0.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MvcIntro0.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreContext context;


        public HomeController(StoreContext cntxt)
            => context = cntxt;


        public IActionResult Index(int id, Purchase purchase, string orderBy = "Line")
        {
            if (purchase.FirstName != null)
            {
                ViewBag.Gratitude = $"Thank you, {purchase.FirstName}, for your order. We love you, come back soon!";
            }


            return orderBy switch
            {
                "Line" => View(context.Bikes.OrderBy(item => item.Line).Skip(id * 10).Take(10).ToList()),
                "Model" => View(context.Bikes.OrderBy(item => item.Model).Skip(id * 10).Take(10).ToList()),
                "Frame" => View(context.Bikes.OrderBy(item => item.Frame).Skip(id * 10).Take(10).ToList()),
                "Fork" => View(context.Bikes.OrderBy(item => item.Fork).Skip(id * 10).Take(10).ToList()),
                "Shifter" => View(context.Bikes.OrderBy(item => item.Shifter).Skip(id * 10).Take(10).ToList()),
                "Brake" => View(context.Bikes.OrderBy(item => item.Brake).Skip(id * 10).Take(10).ToList()),
                "Cost" => View(context.Bikes.OrderBy(item => item.Cost).Skip(id * 10).Take(10).ToList())
            };
        }


        public IActionResult Order(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.BikeId = id;

            return View();
        }


        [HttpPost]
        public IActionResult Order(Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                context.Purchases.Add(purchase);
                context.SaveChanges();

                ConfirmationMailSending(purchase);

                return RedirectToAction("Index", purchase);
            }

            return View(purchase);
        }


        private void ConfirmationMailSending(Purchase purchase)
        {
            SmtpClient SmtpC = new SmtpClient("smpt.gmail.com", 587)
            {
                Credentials = new NetworkCredential("sender", "password"),
                EnableSsl = true
            };
            try
            {
                SmtpC.Send(new MailMessage("sender", "receiver", "Purchase confirmation"
                    , $"Dear {purchase.FirstName}," +
                    $"\nYour purchase of {purchase.Velo.Line} {purchase.Velo.Model} is confirmed." +
                    $"\nThank you. We love you, come back soon!"));
            }
            catch { }
        }


        public IActionResult Privacy()
            => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
