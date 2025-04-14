using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWatchRepair.Data;
using SmartWatchRepair.Models;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly RepairServiceDbContext _context;

    public PaymentsController(RepairServiceDbContext context)
    {
        _context = context;
    }

    // GET: api/Payments - Accessible by Admin and Technician (view all payments)
    [Authorize(Roles = "Admin,Technician")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
    {
        return await _context.Payments.Include(p => p.Booking).ToListAsync();
    }

    // GET: api/Payments/{id} - Accessible by Admin, Technician, and Customer (view specific payment)
    // Customers can only view their own payment.
    [Authorize(Roles = "Admin,Technician,Customer")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Payment>> GetPayment(int id)
    {
        var payment = await _context.Payments.Include(p => p.Booking).FirstOrDefaultAsync(p => p.Id == id);

        // If the user is a Customer, check if the payment belongs to them
        if (User.IsInRole("Customer") && payment?.Booking?.UserId != User.Identity.Name)
        {
            return Forbid(); // If the Customer tries to access a payment that's not theirs
        }

        if (payment == null)
        {
            return NotFound();
        }

        return payment;
    }

    // POST: api/Payments - Accessible by Admin only (Admin creates payments)
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Payment>> PostPayment(Payment payment)
    {
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetPayment", new { id = payment.Id }, payment);
    }

    // PUT: api/Payments/{id} - Accessible by Admin only (Admin updates payments)
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPayment(int id, Payment payment)
    {
        if (id != payment.Id)
        {
            return BadRequest();
        }

        _context.Entry(payment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Payments/{id} - Accessible by Admin only (Admin deletes payments)
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
        {
            return NotFound();
        }

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
