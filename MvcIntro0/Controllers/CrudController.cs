﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIntro0.Models;
using System.Linq;

namespace MvcIntro0.Controllers
{
    [Authorize]
    public class CrudController : Controller
    {
        private readonly StoreContext _context;


        public CrudController(StoreContext cntxt)
            => _context = cntxt;
        

        public IActionResult Index(int id, string orderBy = "Line")
        {

            return View(_context.Bikes
                        .AsEnumerable()
                        .OrderBy(item => typeof(Bike)
                                    .GetProperty(orderBy)
                                    .GetValue(item))
                        .Skip(id * 10).Take(10)
                        .ToList());
        }
        
        public IActionResult Addition(int? id)
            => View(_context.Bikes.Find(id));
        
        public IActionResult AccountRole()
            => View(_context.Users.Include(acnt => acnt.Role).ToList());


        [HttpPost]
        public IActionResult Addition(Bike newBike)
        {
            if (!ModelState.IsValid)
            {
                return View(newBike);
            }
            if (newBike.BikeId != null)
            {
                var currentBike = _context.Bikes.Find(newBike.BikeId);
                currentBike.Line = newBike.Line;
                currentBike.Model = newBike.Model;
                currentBike.Frame = newBike.Frame;
                currentBike.Fork = newBike.Fork;
                currentBike.Shifter = newBike.Shifter;
                currentBike.Brake = newBike.Brake;
                currentBike.Cost = newBike.Cost;
            }
            else
            {
                _context.Bikes.Add(newBike);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Deletion(int id)
        {
            _context.Bikes.Remove(_context.Bikes.Find(id));
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
