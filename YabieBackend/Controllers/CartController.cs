using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YabieBackend.Data;
using YabieBackend.Models;

namespace YabieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly YabieBackendContext _context;

        public CartController(YabieBackendContext context)
        {
            _context = context;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartModel()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Cart/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Cart>> GetCartModel(Guid id)
        {
            var cartModel = await _context.Carts.FindAsync(id);

            if (cartModel == null)
            {
                return NotFound();
            }

            return cartModel;
        }

        // PUT: api/Cart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutCartModel(Guid id, Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartModelExists(id))
                {
                    return NotFound();
                }
                
            }

            return NoContent();
        }

        // POST: api/Cart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCartModel(Cart cart)
        {
            _context.Carts.Add(cart);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CartModelExists(cart.CartId))
                {
                    return Conflict();
                }
                
            }

            return CreatedAtAction("GetCartModel", new { id = cart.CartId }, cart);
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCartModel(Guid id)
        {
            var cartModel = await _context.Carts.FindAsync(id);
            if (cartModel == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cartModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartModelExists(Guid id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}