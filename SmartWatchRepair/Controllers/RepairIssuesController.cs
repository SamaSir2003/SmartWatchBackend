using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatchRepair.Data;
using SmartWatchRepair.Models;


namespace SmartWatchRepair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairIssuesController : ControllerBase
    {
        private readonly RepairServiceDbContext _context;

        public RepairIssuesController(RepairServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/RepairIssues - Accessible by Admin, Technician, and Customer (Read-only access)
        [Authorize(Roles = "Admin,Technician,Customer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepairIssue>>> GetRepairIssues()
        {
            return await _context.RepairIssues.Include(ri => ri.DeviceModel).ToListAsync();
        }

        // GET: api/RepairIssues/{id} - Accessible by Admin, Technician, and Customer (Read-only access)
        [Authorize(Roles = "Admin,Technician,Customer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RepairIssue>> GetRepairIssue(int id)
        {
            var repairIssue = await _context.RepairIssues.Include(ri => ri.DeviceModel).FirstOrDefaultAsync(ri => ri.Id == id);
            if (repairIssue == null)
            {
                return NotFound();
            }
            return repairIssue;
        }

        // POST: api/RepairIssues - Accessible by Admin only (Add a new repair issue)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<RepairIssue>> PostRepairIssue(RepairIssue repairIssue)
        {
            _context.RepairIssues.Add(repairIssue);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRepairIssue", new { id = repairIssue.Id }, repairIssue);
        }

        // PUT: api/RepairIssues/{id} - Accessible by Admin only (Update an existing repair issue)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepairIssue(int id, RepairIssue repairIssue)
        {
            if (id != repairIssue.Id)
            {
                return BadRequest();
            }

            _context.Entry(repairIssue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/RepairIssues/{id} - Accessible by Admin only (Delete a repair issue)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepairIssue(int id)
        {
            var repairIssue = await _context.RepairIssues.FindAsync(id);
            if (repairIssue == null)
            {
                return NotFound();
            }

            _context.RepairIssues.Remove(repairIssue);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}