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
        private readonly ILogger<HomeController> _logger;

        private StoreContext context;
        public HomeController(StoreContext cntxt, UserManager<Account> acntMngr)
        {
            context = cntxt;
            acntMngr.CreateAsync(new Account { UserName = "admin" }, "Admin_1").Wait();
        }

        public IActionResult Index(int id, Purchase purchase)
        {
            if (purchase.FirstName != null)
            {
                ViewBag.Gratitude = $"Thank you, {purchase.FirstName}, for your order. We love you, come back soon!";
            }

            return View(context.Bikes.Skip(id * 10).Take(10).ToList());
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
            SmtpClient SmtpC = new SmtpClient("smpt.gmail.com", 587);
            SmtpC.Credentials = new NetworkCredential("sender", "password");
            SmtpC.EnableSsl = true;
            try
            {
                SmtpC.Send(new MailMessage("sender", "receiver", "Purchase confirmation"
                    , $"Dear {purchase.FirstName}," +
                    $"\nYour purchase of {purchase.Velo.Line} {purchase.Velo.Model} is confirmed." +
                    $"\nThank you. We love you, come back soon!"));
            }
            catch {}        
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
