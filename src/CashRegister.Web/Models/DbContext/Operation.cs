using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CashRegister.Web.Models.DbContext
{
    public class Operation
    {
        public Operation()
        {
            Details = new HashSet<OperationDetail>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Date et heure")]
        public DateTime Moment { get; set; }

        [Required]
        [Display(Name = "Au prix coûtant")]
        public bool Reduced { get; set; }

        [Required]
        [Display(Name = "Détails de l'opération")]
        public virtual ICollection<OperationDetail> Details { get; set; }

        [NotMapped]
        [Display(Name = "Total")]
        public decimal Total => Details.Sum(x => x.TotalPrice);
    }
}
