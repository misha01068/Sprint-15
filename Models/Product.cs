using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProductsValidation.Models
{
    public class Product
    {
        public enum Category { Toy, Technique, Clothes, Transport}

        public int Id { get; set; }
        public Category Type { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "Description must be at least 3 characters long")]
        [DescriptionValidation]
        public string Description { get; set; }
        [Range(typeof(decimal), "0", "100000", ErrorMessage = "Price must be between 0 and 100000")]
        public decimal Price { get; set; }
    }
}
