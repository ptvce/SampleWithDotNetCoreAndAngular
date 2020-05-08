using Microsoft.AspNetCore.Mvc;
using SampleWithDotNetCoreAndAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWithDotNetCoreAndAngular.ViewComponents
{
    public class TopProductsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int count) 
        {
            var topProducts = SampleDB.Products.OrderByDescending(q => q.UnitPrice).Take(count).ToList();
            return View(topProducts);
        }
    }
}
