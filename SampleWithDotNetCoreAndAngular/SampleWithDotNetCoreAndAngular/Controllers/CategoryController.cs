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
        public IActionResult Index()
        {
            return View();
        }
    }
}