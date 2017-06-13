using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CashRegister.Web.DataAccess;
using CashRegister.Web.Models.DbContext;

namespace CashRegister.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products
                .Include(x => x.Category)
                .OrderBy(x => x.Name)
                .ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != product.Id)
                return BadRequest();

            var category = await _context.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("", "Unknown category !");
                return BadRequest(ModelState);
            }

            product.Category = category;
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _context.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("", "Unknown category !");
                return BadRequest(ModelState);
            }

            product.Category = category;
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new {
                id = product.Id
            }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}