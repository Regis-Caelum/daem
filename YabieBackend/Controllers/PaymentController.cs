using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YabieBackend.Data;
using YabieBackend.Models;

namespace YabieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly YabieBackendContext _context;

        public PaymentController(YabieBackendContext context)
        {
            _context = context;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentModel()
        {
            return await _context.Payments.ToListAsync();
        }

        // GET: api/Payment/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Payment>> GetPaymentModel(Guid id)
        {
            var paymentModel = await _context.Payments.FindAsync(id);

            if (paymentModel == null)
            {
                return NotFound();
            }

            return paymentModel;
        }

        // PUT: api/Payment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutPaymentModel(Guid id, Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentModelExists(id))
                {
                    return NotFound();
                }
                
            }

            return NoContent();
        }

        // POST: api/Payment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPaymentModel(Payment payment)
        {
            _context.Payments.Add(payment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PaymentModelExists(payment.PaymentId))
                {
                    return Conflict();
                }
                
            }

            return CreatedAtAction("GetPaymentModel", new { id = payment.PaymentId }, payment);
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePaymentModel(Guid id)
        {
            var paymentModel = await _context.Payments.FindAsync(id);
            if (paymentModel == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(paymentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentModelExists(Guid id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}