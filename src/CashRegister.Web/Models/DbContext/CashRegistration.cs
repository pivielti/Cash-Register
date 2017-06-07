using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CashRegister.Web.Models.DbContext
{
    public class CashRegistration : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Moment { get; set; }

        [Required]
        [Display(Name = "Type d'enregistrement")]
        public RegistrationType Type { get; set; }

        [Required]
        [Display(Name = "Contenu de la caisse")]
        public double Cash { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Cash <= 0)
                yield return new ValidationResult("Le montant doit être suppérieur à zéro !", new[] { nameof(Cash) });
        }
    }

    public enum RegistrationType
    {
        [Display(Name = "Ouverture")]
        Opening,
        [Display(Name = "Fermeture")]
        Closing
    }
}
