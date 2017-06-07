using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegister.Web.Models.DbContext
{
    public class OperationDetail
    {
        public int Id { get; set; }

        [Display(Name = "Nom du produit")]
        public string ProductName { get; set; }

        [Display(Name = "Prix du produit")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Nombre")]
        public int Count { get; set; }

        public int OperationId { get; set; }

        [Display(Name = "Opération")]
        public virtual Operation Operation { get; set; }

        [NotMapped]
        [Display(Name = "Total de l'opération")]
        public decimal TotalPrice => ProductPrice * Count;
    }
}
