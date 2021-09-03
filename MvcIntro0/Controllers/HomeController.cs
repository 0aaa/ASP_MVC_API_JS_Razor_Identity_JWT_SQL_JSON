using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MvcIntro0.Models;
using MvcIntro0.Models.ViewModels;
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


        public IActionResult Index(int id, Purchase purchase, string orderBy = "Line", string line = "Select", string model = "Select", string frame = "Select",
            string fork = "Select", string shifter = "Select", string brake = "Select", string cost = "Select")
        {
            if (purchase.FirstName != null)
            {
                ViewBag.Gratitude = $"Thank you, {purchase.FirstName}, for your order. We love you, come back soon!";
            }

            var items = context.Bikes
                        .AsEnumerable()
                        .OrderBy(item => typeof(Bike)
                                    .GetProperty(orderBy)
                                    .GetValue(item))
                        .Skip(id * 10).Take(10)
                        .ToList();


            var propertyNames = new List<string>();
            var propertySelect_s = new List<List<string>>();


            var selectedItems = items;
            string[] arguments = { line, model, frame, fork, shifter, brake, cost };


            var returnParameter = new ItemsViewModel();


            for (int i = 0; i < typeof(Bike).GetProperties().Length - 1; i++)
            {
                propertyNames.Add(typeof(Bike).GetProperties()[i + 1].Name);

                propertySelect_s.Add(new List<string> { "Select" });

                propertySelect_s[i].AddRange(items.Select(item
                                                        => $"{typeof(Bike).GetProperty(propertyNames[i]).GetValue(item)}")
                                                        .Distinct());


                selectedItems = arguments[i] == "Select"
                                ? selectedItems
                                : selectedItems.Where(item
                                                => $"{typeof(Bike).GetProperty(propertyNames[i]).GetValue(item)}" == arguments[i])
                                                .ToList();


                returnParameter.GetType()
                                .GetProperties()[i]
                                .SetValue(returnParameter, new SelectList(propertySelect_s[i]));
            }

            returnParameter.Items = selectedItems;

            return View(returnParameter);
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
            var SmtpC = new SmtpClient("smpt.gmail.com", 587)
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
