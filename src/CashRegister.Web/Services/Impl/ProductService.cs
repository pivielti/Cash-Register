using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CashRegister.Web.Models.DbContext;
using CashRegister.Web.DataAccess;

namespace CashRegister.Web.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.Include(x => x.Category).ToList();
        }

        public IEnumerable<Product> GetTopSellProducts()
        {
            return null;
        }
    }
}
