using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YabieBackend.Data;
using YabieBackend.Models;

namespace YabieBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly YabieBackendContext _context;

        public ProductController(YabieBackendContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductModel()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Product>> GetProductModel(Guid id)
        {
            var productModel = await _context.Products.FindAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return productModel;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutProductModel(Guid id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
                {
                    return NotFound();
                }
                
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProductModel(Product product)
        {
            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductModelExists(product.ProductId))
                {
                    return Conflict();
                }
            }
            return CreatedAtAction("GetProductModel", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProductModel(Guid id)
        {
            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductModelExists(Guid id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> ListActiveProducts()
        {
            // Filter products where at least one associated auction has an active status.
            var activeProducts = await _context.Products
                .Join(_context.Auctions, product => product.ProductId, auction => auction.ProductRefId,
                    (product, auction) => new { product, auction.Status })
                .Where(record => record.Status == Status.OPEN)
                .Select(record => record.product)
                .ToListAsync();
 
            return activeProducts;
        }
    }
}