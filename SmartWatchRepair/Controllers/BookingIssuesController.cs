using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatchRepair.Data;
using SmartWatchRepair.Models;


namespace SmartWatchRepair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly RepairServiceDbContext _context;

        public BookingsController(RepairServiceDbContext context)
        {
            _context = context;
        }


        
        // GET: api/Bookings - Accessible by Admin only
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            // Fetch bookings for Admin
            var bookings = await _context.Bookings.ToListAsync();

            // Check if bookings are retrieved successfully
            if (bookings == null || bookings.Count == 0)
            {
                return NotFound(new { message = "No bookings found." });
            }

            // Return successful response with data
            return Ok(new
            {
                message = "Bookings successfully retrieved.",
                status = "success",
                data = bookings
            });
        }


        // GET: api/Bookings/{id} - Accessible by Admin only
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // POST: api/Bookings - Accessible by Admin, Technician, and Customer
        [Authorize(Roles = "Admin,Technician,Customer")]
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking([FromBody] Booking booking)
        {
            // Validate if the User exists
            var userExists = await _context.Users.AnyAsync(u => u.Id == booking.UserId);
            if (!userExists)
            {
                return BadRequest("User not found.");
            }

            // Validate if the DeviceModel exists
            var deviceModelExists = await _context.DeviceModels.AnyAsync(dm => dm.Id == booking.DeviceModelId);
            if (!deviceModelExists)
            {
                return BadRequest("Device model not found.");
            }

            // Validate if the RepairIssue exists
            var repairIssueExists = await _context.RepairIssues.AnyAsync(ri => ri.Id == booking.RepairIssueId);
            if (!repairIssueExists)
            {
                return BadRequest("Repair issue not found.");
            }

            // Validate if the Technician exists
            var technicianExists = await _context.Users.AnyAsync(u => u.Id == booking.TechnicianId && u.Role == "Technician");
            if (!technicianExists)
            {
                return BadRequest("Technician not found.");
            }

            // Add the booking if all validations pass
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // PUT: api/Bookings/{id} - Accessible by Admin and Technician
        [Authorize(Roles = "Admin,Technician")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Bookings/{id} - Accessible by Admin only
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}