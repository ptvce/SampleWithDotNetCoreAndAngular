using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Models
{
    public class ProductModel
    {
        [Required]
        public int ProductId { get; set; }
     
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [Range(10,500)]
        public int UnitPrice { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
