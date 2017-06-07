﻿using System.ComponentModel.DataAnnotations;

namespace CashRegister.Web.Models.DbContext
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }
    }
}
