using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CashRegister.Web.DataAccess;
using Microsoft.EntityFrameworkCore;
using CashRegister.Web.Models.DbContext;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CashRegister.Web.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .Include(x => x.Category)
                .OrderBy(x => x.Name)
                .ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        [Route("")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("", "Unknown category !");
                return BadRequest(ModelState);
            }

            _context.Add(product);

            await _context.SaveChangesAsync();

            var location = Url.Action(nameof(GetById), new {
                id = product.Id
            });

            return Created(location, product);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(Product product)
        {
            var attached = _context.Products.Attach(product);
            attached.State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
