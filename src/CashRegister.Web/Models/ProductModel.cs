using System.ComponentModel.DataAnnotations;

namespace CashRegister.Web.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Prix")]
        public double? Price { get; set; }

        [Required]
        [Display(Name = "Prix coûtant")]
        public double? CostPrice { get; set; }

        [Required]
        [Display(Name = "Catégorie")]
        public int? CategoryId { get; set; }
    }
}
