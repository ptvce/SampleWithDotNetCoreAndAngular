using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWithDotNetCoreAndAngular.Helper;

namespace SampleWithDotNetCoreAndAngular.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryHelper _categoryHelper;
        public CategoryController(ICategoryHelper categoryHelper)
        {
            _categoryHelper = categoryHelper;
        }

        //from query string => http://localhost/product?code=10&page=1
        // from routing => http://localhost/product/10/1

        public async Task<IActionResult> Index([FromQuery]int page=1)
        {
            var result = await _categoryHelper.GetWithPaginationAsync(page);
            return View(result);
        }
    }
}