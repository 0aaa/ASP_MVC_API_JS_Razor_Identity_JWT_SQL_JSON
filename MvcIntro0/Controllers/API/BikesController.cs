using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcIntro0.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class BikesController : Controller
    {
        private readonly StoreContext _context;


        public BikesController(StoreContext context)
            => _context = context;


        public async Task<ActionResult<IEnumerable<Bike>>> Get()
            => await _context.Bikes.ToListAsync();


        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> Get(int id)
        {
            var currentItem = await _context.Bikes.FirstOrDefaultAsync(item => item.BikeId == id);

            if (currentItem != null)
            {
                return currentItem;
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult<Bike>> Post(Bike item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();

                return Ok(item);
            }

            return BadRequest(ModelState);
        }


        [HttpPut]
        public async Task<ActionResult<Bike>> Put(Bike item)
        {
            if (_context.Bikes.Any(bike => bike.BikeId == item.BikeId))
            {
                _context.Update(item);
                await _context.SaveChangesAsync();

                return Ok(item);
            }

            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Bike>> Delete(int id)
        {
            var currentItem = await _context.Bikes.FirstOrDefaultAsync(item => item.BikeId == id);

            if (currentItem != null)
            {
                _context.Remove(await _context.Bikes.FirstOrDefaultAsync(item => item.BikeId == id));
                await _context.SaveChangesAsync();

                return Ok();
            }

            return NotFound();
        }
    }
}
