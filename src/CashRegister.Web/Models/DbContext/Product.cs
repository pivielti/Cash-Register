﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashRegister.Web.Models.DbContext
{
    public class Product : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal CostPrice { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price < 0)
                yield return new ValidationResult("Ce champ doit être suppérieur ou égal à 0.", new[] { nameof(Price) });
            if (CostPrice < 0)
                yield return new ValidationResult("Ce champ doit être suppérieur ou égal à 0.", new[] { nameof(CostPrice) });
        }
    }
}
