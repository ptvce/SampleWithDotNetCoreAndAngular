using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleWithDotNetCoreAndAngular.Common;
using SampleWithDotNetCoreAndAngular.Entites;
using SampleWithDotNetCoreAndAngular.Helper;
using SampleWithDotNetCoreAndAngular.Repository;
using SampleWithDotNetCoreAndAngular.ViewModels;

namespace SampleWithDotNetCoreAndAngular.Controllers
{
    public class ProductWithRepositoryController : Controller
    {
        IGenericRepository<Products> _gerericProductRepository;
        IGenericRepository<Categories> _genericCategoryRepository;
        public ProductWithRepositoryController(IGenericRepository<Products> gerericProductRepository, IGenericRepository<Categories> genericCategoryRepository)
        {
            _gerericProductRepository = gerericProductRepository;
            _genericCategoryRepository = genericCategoryRepository;
        }
        public async Task<IActionResult> Index(string productName = null, int? categoryId = null,
            int? fromPrice = null, int? toPrice = null, 
            [FromQuery]int page = 1, [FromQuery]int limit = 5)
        {
            var repoResult = await _gerericProductRepository.GetAsQueryableAsync(q =>
            (string.IsNullOrEmpty(productName) ? true : q.ProductName.Contains(productName)) &&
            (categoryId == null ? true : q.CategoryId == categoryId) &&
            (fromPrice == null ? true : q.UnitPrice >= fromPrice) && (toPrice == null ? true : q.UnitPrice <= toPrice),

            page: page, limit: limit);
            var result = new ServicePaginationResult<ProductViewModel>();
            result.Limit = repoResult.Limit;
            result.Page = repoResult.Page;
            result.Pages = repoResult.Pages;
            result.Total = repoResult.Total;
            result.Data = await repoResult.Data.Select(q => new ProductViewModel
            {
                CategoryId = q.CategoryId,
                ProductId = q.ProductId,
                CategoryName = q.Category.CategoryName,
                ProductName = q.ProductName,
                UnitPrice = q.UnitPrice.Value
            }).ToListAsync();
            var categoriesRepoResult = await _genericCategoryRepository.GetAsQueryableAsync(null, q => q.OrderBy(c => c.CategoryName));
            var categories = await categoriesRepoResult.Data.Select(q => new SelectListItem
            {
                Selected = q.CategoryId == categoryId,
                Text = q.CategoryName,
                Value = q.CategoryId.ToString()
            }).ToListAsync();

            ViewData["Categories"] = categories;
            ViewData["ProductName"] = productName;
            ViewData["FromPrice"] = fromPrice;
            ViewData["ToPrice"] = toPrice;
            ViewData["QueryString"] = HttpContext.Request.QueryString.Value;

            return View(result);
        }
    }
}