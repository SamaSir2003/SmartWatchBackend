using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatchRepair.Data;
using SmartWatchRepair.Models;
using Microsoft.AspNetCore.Authorization;

namespace SmartWatchRepair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly RepairServiceDbContext _context;

        public BrandsController(RepairServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Brands - Accessible by Admin, Technician, and Customer (Read-only access)
        [Authorize(Roles = "Admin,Technician,Customer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        // GET: api/Brands/{id} - Accessible by Admin, Technician, and Customer (Read-only access)
        [Authorize(Roles = "Admin,Technician,Customer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return brand;
        }

        // POST: api/Brands - Accessible by Admin only (Add a new brand)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }

        // PUT: api/Brands/{id} - Accessible by Admin only (Update an existing brand)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Brands/{id} - Accessible by Admin only (Delete a brand)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}