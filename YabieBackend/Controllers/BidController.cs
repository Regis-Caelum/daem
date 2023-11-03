using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YabieBackend.Data;
using YabieBackend.Models;

namespace YabieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly YabieBackendContext _context;

        public BidController(YabieBackendContext context)
        {
            _context = context;
        }

        // GET: api/Bid
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidModel()
        {
            return await _context.Bids.ToListAsync();
        }

        // GET: api/Bid/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Bid>> GetBidModel(Guid id)
        {
            var bidModel = await _context.Bids.FindAsync(id);

            if (bidModel == null)
            {
                return NotFound();
            }

            return bidModel;
        }

        // PUT: api/Bid/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutBidModel(Guid id, Bid bid)
        {
            if (id != bid.BidId)
            {
                return BadRequest();
            }

            _context.Entry(bid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidModelExists(id))
                {
                    return NotFound();
                }
                
            }

            return NoContent();
        }

        // POST: api/Bid
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bid>> PostBidModel(Bid bid)
        {
            _context.Bids.Add(bid);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BidModelExists(bid.BidId))
                {
                    return Conflict();
                }
                
            }

            return CreatedAtAction("GetBidModel", new { id = bid.BidId }, bid);
        }

        // DELETE: api/Bid/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBidModel(Guid id)
        {
            var bidModel = await _context.Bids.FindAsync(id);
            if (bidModel == null)
            {
                return NotFound();
            }

            _context.Bids.Remove(bidModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidModelExists(Guid id)
        {
            return _context.Bids.Any(e => e.BidId == id);
        }
    }
}