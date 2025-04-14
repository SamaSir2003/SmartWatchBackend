using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatchRepair.Data;
using SmartWatchRepair.Models;

namespace SmartWatchRepair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceModelsController : ControllerBase
    {
        private readonly RepairServiceDbContext _context;

        public DeviceModelsController(RepairServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/DeviceModels - Accessible by Admin, Technician, and Customer (Read-only access)
        [Authorize(Roles = "Admin,Technician,Customer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceModel>>> GetDeviceModels()
        {
            return await _context.DeviceModels.Include(dm => dm.Brand).ToListAsync();
        }

        // GET: api/DeviceModels/{id} - Accessible by Admin, Technician, and Customer (Read-only access)
        [Authorize(Roles = "Admin,Technician,Customer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceModel>> GetDeviceModel(int id)
        {
            var deviceModel = await _context.DeviceModels.Include(dm => dm.Brand).FirstOrDefaultAsync(dm => dm.Id == id);
            if (deviceModel == null)
            {
                return NotFound();
            }
            return deviceModel;
        }

        // POST: api/DeviceModels - Accessible by Admin only (Add a new device model)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DeviceModel>> PostDeviceModel(DeviceModel deviceModel)
        {
            _context.DeviceModels.Add(deviceModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDeviceModel", new { id = deviceModel.Id }, deviceModel);
        }

        // PUT: api/DeviceModels/{id} - Accessible by Admin only (Update an existing device model)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceModel(int id, DeviceModel deviceModel)
        {
            if (id != deviceModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(deviceModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/DeviceModels/{id} - Accessible by Admin only (Delete a device model)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceModel(int id)
        {
            var deviceModel = await _context.DeviceModels.FindAsync(id);
            if (deviceModel == null)
            {
                return NotFound();
            }

            _context.DeviceModels.Remove(deviceModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}