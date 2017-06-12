using CashRegister.Web.Models.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashRegister.Web.Models
{
    public class OperationGroupModel
    {
        public OperationGroupModel()
        {
            Operations = new List<Operation>();
        }

        public DateTime Date { get; set; }

        public ICollection<Operation> Operations { get; set; }

        public decimal? StartCash { get; set; }

        public decimal? EndCash { get; set; }

        public decimal Total => GetTotal();

        public decimal GetTotal()
        {
            return Operations.SelectMany(x => x.Details).Sum(x => x.ProductPrice * x.Count);
        }
    }
}
