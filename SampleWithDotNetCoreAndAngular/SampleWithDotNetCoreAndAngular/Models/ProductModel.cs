using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Models
{
    public class ProductModel
    {
        //[Display(Name = nameof(Titles.ProductId), ResourceType = typeof(Titles))]
        public int ProductId { get; set; }

        //[Display(Name = nameof(Titles.ProductName), ResourceType = typeof(Titles))]
        //[Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        //[StringLength(64, MinimumLength = 3, ErrorMessageResourceName = nameof(Messages.StringLength), ErrorMessageResourceType = typeof(Messages))]
        public string ProductName { get; set; }
        //[Display(Name = nameof(Titles.CategoryId), ResourceType = typeof(Titles))]
        //[Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        public int CategoryId { get; set; }
        //[Display(Name = nameof(Titles.UnitPrice), ResourceType = typeof(Titles))]
        //[Required(ErrorMessageResourceName = nameof(Messages.Required), ErrorMessageResourceType = typeof(Messages))]
        //[Range(10, 500)]
        public int UnitPrice { get; set; }
        //[EmailAddress]
        //[Display(Name = nameof(Titles.Email), ResourceType = typeof(Titles))]
        public string Email { get; set; }
    }
}
