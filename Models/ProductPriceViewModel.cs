using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsValidation.Models
{
    public class ProductPriceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "100000",
            ErrorMessage = "Price must be between 0 and 100000")]
        public decimal Price { get; set; }
    }
}