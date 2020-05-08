using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.Models
{
    //just for sample I use static Data
    public class SampleDB
    {
        public static List<ProductModel> Products = new List<ProductModel>
        {
        new ProductModel { ProductId = 1, CategoryId = 10, ProductName = "AAA", UnitPrice = 200},
        new ProductModel { ProductId = 2, CategoryId = 11, ProductName = "AAA", UnitPrice = 200},
        new ProductModel { ProductId = 3, CategoryId = 12, ProductName = "AAA", UnitPrice = 200},
        new ProductModel { ProductId = 4, CategoryId = 13, ProductName = "AAA", UnitPrice = 200},
        new ProductModel { ProductId = 5, CategoryId = 14, ProductName = "AAA", UnitPrice = 200},
        new ProductModel { ProductId = 6, CategoryId = 15, ProductName = "AAA", UnitPrice = 200}

        };

        public static List<CategoryModel> Categories = new List<CategoryModel>
        {
            new CategoryModel { CategoryId = 1, CategoryName = "Mobile"},
            new CategoryModel { CategoryId = 2, CategoryName = "Tablet"},
            new CategoryModel { CategoryId = 3, CategoryName = "Laptop"}
        };

    }
}
