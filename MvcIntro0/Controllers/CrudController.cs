using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIntro0.Models;
using System.IO;
using System.Linq;

namespace MvcIntro0.Controllers
{
    [Authorize]
    public class CrudController : Controller
    {
        private readonly StoreContext _context;


        public CrudController(StoreContext cntxt)
        {
            _context = cntxt;
        }


        public IActionResult Index()
        {
            return View(_context.Bikes.ToList());
        }

        public IActionResult Addition(int? id)
        {
            return View(_context.Bikes.Find(id));
        }

        public IActionResult AccountRole()
        {
            return View(_context.Users.Include(acnt => acnt.Role).ToList());
        }

        [HttpPost]
        public IActionResult Addition(Bike newBike, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View(newBike);
            }
            var mmryStrm = new MemoryStream();
            image.CopyTo(mmryStrm);

            newBike.Image64 = mmryStrm.ToArray();

            if (newBike.BikeId != null)
            {

                var currentBike = _context.Bikes.Find(newBike.BikeId);
                currentBike.Line = newBike.Line;
                currentBike.Model = newBike.Model;
                currentBike.Image64 = newBike.Image64;
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
