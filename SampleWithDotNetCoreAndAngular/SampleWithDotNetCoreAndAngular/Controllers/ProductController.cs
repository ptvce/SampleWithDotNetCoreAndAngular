using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWithDotNetCoreAndAngular.Helper;
using SampleWithDotNetCoreAndAngular.Models;

namespace SampleWithDotNetCoreAndAngular.Controllers
{
    public class ProductController : Controller
    {
        ICategoryHelper _categoryHelper;
        IProductHelper _productHelper;
        public ProductController(ICategoryHelper categoryHelper, IProductHelper productHelper)
        {
            _categoryHelper = categoryHelper;
            _productHelper = productHelper;
        }
        public async Task<IActionResult> Index()
        {
            //var products = SampleDB.Products;
            var products = await _productHelper.GetAllAsync();
            // we can use ViewBag or ViewData for send data 
            //from action to view, we just use one of them
            ViewBag.Name = "My Product";
            ViewData["Name"] = "Product";
            return View(products);
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
            await _productHelper.AddAsync(model);
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categories"] = SampleDB.Categories;
            var model = SampleDB.Products.FirstOrDefault(q => q.ProductId == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductModel model)
        {
            var index = SampleDB.Products.FindIndex(q => q.ProductId == id);
            if (index != -1) 
            {
                model.ProductId = id;
                SampleDB.Products[index] = model;
                return RedirectToAction("Index");
            }
            return View(model);
           
        }
        #endregion

        #region Delete
        public IActionResult Delete(int id)
        {
            var index = SampleDB.Products.FindIndex(q => q.ProductId == id);
            if (index != -1)
            {
                SampleDB.Products.RemoveAt(index);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}