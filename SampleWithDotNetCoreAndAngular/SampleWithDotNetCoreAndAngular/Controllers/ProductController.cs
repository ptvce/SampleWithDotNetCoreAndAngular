using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleWithDotNetCoreAndAngular.Models;

namespace SampleWithDotNetCoreAndAngular.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var products = SampleDB.Products;
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
        public IActionResult Create()
        {
            ViewData["Categories"] = SampleDB.Categories;
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            SampleDB.Products.Add(model);
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