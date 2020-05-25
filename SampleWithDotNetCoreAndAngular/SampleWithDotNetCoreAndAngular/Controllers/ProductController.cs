using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleWithDotNetCoreAndAngular.Common;
using SampleWithDotNetCoreAndAngular.Data;
using SampleWithDotNetCoreAndAngular.Entites;
using SampleWithDotNetCoreAndAngular.Helper;
using SampleWithDotNetCoreAndAngular.Models;

namespace SampleWithDotNetCoreAndAngular.Controllers
{
    public class ProductController : Controller
    {
        ICategoryHelper _categoryHelper;
        IProductHelper _productHelper;
        ILogger<ProductModel> _logger;
        CoreLearningContext _coreLearningContext;
        public ProductController(ICategoryHelper categoryHelper, IProductHelper productHelper, ILogger<ProductModel> logger, CoreLearningContext coreLearningContext)
        {
            _categoryHelper = categoryHelper;
            _productHelper = productHelper;
            _logger = logger;
            _coreLearningContext = coreLearningContext;
        }
        [Route("Product")]
        [Route("Product/Index")]
        [Route("Product/showall/{page?}")]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("show product list with browser:" + Request.Headers["User-Agent"].ToString());
            //var products = SampleDB.Products;
            
            //with dapper
            var products = await _productHelper.GetAllAsync();
            var (productss, categories) = await _productHelper.GetMultipleProduct_CategoryAsync();

            //with ef core
            var result = _coreLearningContext.Products.ToList();
                //or
            var result2 = _coreLearningContext.Products.Select(q => new ProductModel
            {
                CategoryId = q.CategoryId,
                ProductName = q.ProductName,
                UnitPrice = q.UnitPrice.Value,
                ProductId = q.ProductId
            });
            // we can use ViewBag or ViewData for send data 
            //from action to view, we just use one of them
            ViewBag.Name = "My Product";
            ViewData["Name"] = "Product";
            return View(result2);
        }

        /// <summary>
        /// Address: /Product/Display
        /// </summary>
        /// <returns></returns>
        public IActionResult Display()
        {
            return Json(SampleDB.Products);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ModelState.AddModelError("", "زمان ثبت نام گذشته");
            ModelState.AddModelError("ProductName", "اجباری است");

            //ViewData["Categories"] = SampleDB.Categories;
            var categories =await _categoryHelper.GetAllAsync();
            ViewData["Categories"] = categories;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductModel model)
        {
          if (!ModelState.IsValid)
            {
                // ViewData["Categories"] = SampleDB.Categories;
                var categories = await _categoryHelper.GetAllAsync();
                ViewData["Categories"] = categories;

                return View(model);
            }

            // SampleDB.Products.Add(model);
            //await _productHelper.AddAsync(model);

            // with dapper
            await _productHelper.AddWithSPAsync(model);

            //with ef core
            var productModel = new Products { 
                CategoryId = model.CategoryId,
                ProductName = model.ProductName,
                UnitPrice = model.UnitPrice
            };

            //syntax1
            await _coreLearningContext.AddAsync(productModel);
            //syntax2
            //await _coreLearningContext.Products.AddAsync(productModel);

            await _coreLearningContext.SaveChangesAsync();
           

            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Categories"] = SampleDB.Categories;
            // var model = SampleDB.Products.FirstOrDefault(q => q.ProductId == id);
           var model = await  _productHelper.GetAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductModel model)
        {
            var index = SampleDB.Products.FindIndex(q => q.ProductId == id);
            if (index != -1) 
            {
                model.ProductId = id;
                //SampleDB.Products[index] = model;

                //with ef core
                //step1: get item from db
                var product = await _coreLearningContext.Products.SingleOrDefaultAsync(q => q.ProductId == id);

                //step2: set new value , entity state = modified
                product.ProductName = model.ProductName;
                product.UnitPrice = model.UnitPrice;
                product.CategoryId = model.CategoryId;

                //step3: save change
                await _coreLearningContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
           
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var index = SampleDB.Products.FindIndex(q => q.ProductId == id);
            if (index != -1)
            {
                //SampleDB.Products.RemoveAt(index);

                //with ef core
                //step1 : get item from db
                var product = await _coreLearningContext.Products.SingleOrDefaultAsync(q => q.ProductId == id);

                //step2: remove product from dbcontext > change state to deleted
                _coreLearningContext.Products.Remove(product);

                //step3: save change
                await  _coreLearningContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}