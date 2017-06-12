using System.Collections.Generic;
using CashRegister.Web.Models.DbContext;

namespace CashRegister.Web.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
    }
}
