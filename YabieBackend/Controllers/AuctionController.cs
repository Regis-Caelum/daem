using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YabieBackend.Data;
using YabieBackend.Models;

namespace YabieBackend.Controllers
{
    // [Authorize(AuthenticationSchemes = "FirebaseAuthScheme")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly YabieBackendContext _context;

        public AuctionController(YabieBackendContext context)
        {
            _context = context;
        }

        // GET: api/Auction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctionModel()
        {
            return await _context.Auctions.ToListAsync();
        }

        // GET: api/Auction/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Auction>> GetAuctionModel(Guid id)
        {
            var auctionModel = await _context.Auctions.FindAsync(id);

            if (auctionModel == null)
            {
                return NotFound();
            }

            return auctionModel;
        }

        // PUT: api/Auction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAuctionModel(Guid id, Auction auction)
        {
            if (id != auction.AuctionId)
            {
                return BadRequest();
            }

            _context.Entry(auction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionModelExists(id))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // POST: api/Auction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Auction>> PostAuctionModel(Auction auction)
        {
            _context.Auctions.Add(auction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuctionModelExists(auction.AuctionId))
                {
                    return Conflict();
                }
            }

            return CreatedAtAction("GetAuctionModel", new { id = auction.AuctionId }, auction);
        }

        // DELETE: api/Auction/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAuctionModel(Guid id)
        {
            var auctionModel = await _context.Auctions.FindAsync(id);
            if (auctionModel == null)
            {
                return NotFound();
            }

            _context.Auctions.Remove(auctionModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuctionModelExists(Guid id)
        {
            return _context.Auctions.Any(e => e.AuctionId == id);
        }

        // GET: api/Auction
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<Auction>>> ListUserAuctions(Guid id)
        {
            return await _context.Auctions
                .Join(_context.UserAuctions, auction => auction.AuctionId, userAuction => userAuction.AuctionId,
                    (auction, userAuction) => new { auction, userAuction.UserId })
                .Where(record => record.UserId == id)
                .Select(record => record.auction)
                .ToListAsync();
        }
    }
}