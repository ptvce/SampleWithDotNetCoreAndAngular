using System;
using System.Collections.Generic;

namespace SampleWithDotNetCoreAndAngular.Entites
{
    public partial class Products
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public int? UnitPrice { get; set; }
    }
}
