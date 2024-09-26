﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnv;

        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnv)
        {
            _unitOfWork = db;
            _webHostEnv = webHostEnv;
        }

        public ActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            
            return View(objProductList);
        }

        public ActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });

            ProductVM _productVm = new ProductVM()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                //Create
                return View(_productVm);
            }
            else
            {
                //Update
                _productVm.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
                return View(_productVm);
            }

        }
        [HttpPost]
        public ActionResult Upsert(ProductVM obj, IFormFile? file) {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnv.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(obj.Product.ImgUrl)){
                        //delete old img
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Product.ImgUrl = @"\images\product\" + fileName;
                }
                if (obj.Product.ProductId == 0)
                {
                    // Add new product
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    // Update existing product
                    _unitOfWork.Product.Update(obj.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });

                obj.CategoryList = CategoryList;
                return View(obj);
            }
        }
        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.GetFirstOrDefault(u => u.ProductId == id);
            if (obj == null)
            {
                NotFound();
            }
            else
            {
                _unitOfWork.Product.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product deleted successfully";
            }

            return RedirectToAction("Index");
        }

        #region Api Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }
        #endregion
    }
}
