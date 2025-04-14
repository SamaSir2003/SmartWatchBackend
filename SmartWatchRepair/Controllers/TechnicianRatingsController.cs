using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatchRepair.Data;
using SmartWatchRepair.Models;


namespace SmartWatchRepair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianRatingsController : ControllerBase
    {
        private readonly RepairServiceDbContext _context;

        public TechnicianRatingsController(RepairServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/TechnicianRatings - Accessible by Admin and Technician (view all ratings)
        [Authorize(Roles = "Admin,Technician")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TechnicianRating>>> GetTechnicianRatings()
        {
            return await _context.TechnicianRatings.Include(tr => tr.Booking).Include(tr => tr.Technician).ToListAsync();
        }

        // GET: api/TechnicianRatings/{id} - Accessible by Admin and Technician (view specific rating)
        [Authorize(Roles = "Admin,Technician")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TechnicianRating>> GetTechnicianRating(int id)
        {
            var technicianRating = await _context.TechnicianRatings.Include(tr => tr.Booking).Include(tr => tr.Technician).FirstOrDefaultAsync(tr => tr.Id == id);
            if (technicianRating == null)
            {
                return NotFound();
            }
            return technicianRating;
        }

        // POST: api/TechnicianRatings - Accessible by Admin only (Add a new technician rating)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<TechnicianRating>> PostTechnicianRating(TechnicianRating technicianRating)
        {
            _context.TechnicianRatings.Add(technicianRating);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTechnicianRating", new { id = technicianRating.Id }, technicianRating);
        }

        // PUT: api/TechnicianRatings/{id} - Accessible by Admin only (Update an existing rating)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechnicianRating(int id, TechnicianRating technicianRating)
        {
            if (id != technicianRating.Id)
            {
                return BadRequest();
            }

            _context.Entry(technicianRating).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/TechnicianRatings/{id} - Accessible by Admin only (Delete a technician rating)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnicianRating(int id)
        {
            var technicianRating = await _context.TechnicianRatings.FindAsync(id);
            if (technicianRating == null)
            {
                return NotFound();
            }

            _context.TechnicianRatings.Remove(technicianRating);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}