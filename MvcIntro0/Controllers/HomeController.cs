﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcIntro0.Models;
using MvcIntro0.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace MvcIntro0.Controllers
{
	public class HomeController : Controller
	{
		private readonly StoreContext context;

		private const int AMOUNTPERPAGE = 10;


		public HomeController(StoreContext cntxt)
        {
			context = cntxt;
        }


		public IActionResult Index(Purchase purchase)
		{
			if (purchase.FirstName != null)
			{
				ViewBag.Gratitude = $"Thank you, {purchase.FirstName}, for your order. We love you, come back soon!";
			}


			var items = context.Bikes
				.AsEnumerable()
				.OrderBy(item => item.Line)
				.ToList();


			var propertySelect_s = new List<List<string>>();
			var returnParameter = new ItemsViewModel
			{
				ItemPagesTotalAmount = context.Bikes.Count() / 10,
				Items = ((ItemsViewModel)(((PartialViewResult)Items()).Model)).Items
			};


			for (int i = 0; i < typeof(Bike).GetProperties().Length - 2; i++)// 1 => 2 OK
			{
				propertySelect_s.Add(new List<string> { "Select" });

				propertySelect_s[i].AddRange(items.Select(item
													=> $"{typeof(Bike).GetProperties()[i + 2].GetValue(item)}")// 1 => 2 OK
												.Distinct());

				typeof(ItemsViewModel).GetProperties()[i]
								.SetValue(returnParameter, new SelectList(propertySelect_s[i]));
			}


			return View(returnParameter);
		}



		public IActionResult Items(string orderBy = "Line", string searchBy = "", int itemsCurrentPage = 1, string line = "Select", string model = "Select", string frame = "Select",
			string fork = "Select", string shifter = "Select", string brake = "Select", string cost = "Select")
		{

			var items = context.Bikes
						.AsEnumerable()
						.Skip((itemsCurrentPage - 1) * AMOUNTPERPAGE)
						.Take(AMOUNTPERPAGE)
						.OrderBy(item => typeof(Bike)
									.GetProperty(orderBy)
									.GetValue(item))
						.ToList();


			string[] arguments = { line, model, frame, fork, shifter, brake, cost };

			var returnParameter = new ItemsViewModel
			{
				ItemPagesTotalAmount = context.Bikes.Count() / AMOUNTPERPAGE,
				ItemsCurrentPage = itemsCurrentPage
			};

			searchBy = searchBy?.ToLower();



			if (arguments.Any(arg => arg != "Select"))
            {
                items = Filtrate(items, arguments);
            }


            if (!string.IsNullOrEmpty(searchBy))
            {
                Find(searchBy, items, returnParameter);

                returnParameter.Items = returnParameter.Items.Distinct().ToList();
            }
            else
			{
				returnParameter.Items = items;
			}


			return PartialView(returnParameter);
		}


        private static List<Bike> Filtrate(List<Bike> items, string[] arguments)
        {
            for (int i = 0; i < typeof(Bike).GetProperties().Length - 2; i++)// 1 => 2 OK
            {
                if (arguments[i] != "Select")
                {
                    items = items.Where(item
                                    => $"{typeof(Bike).GetProperties()[i + 2].GetValue(item)}" == arguments[i])// OK
                                .ToList();
                }
            }

            return items;
        }


        private static void Find(string searchBy, List<Bike> items, ItemsViewModel returnParameter)
        {
            for (int i = 0; i < typeof(Bike).GetProperties().Length - 2; i++)// 1 => 2 OK
            {
                returnParameter.Items.AddRange(items.Where(item
                                                => $"{typeof(Bike).GetProperties()[i + 2].GetValue(item)}"// OK
                                                .ToLower()
                                                .Contains(searchBy)));
            }
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
