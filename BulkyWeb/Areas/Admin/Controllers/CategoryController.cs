
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        //creating new category

        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj); //add category obj to category table using entity framework
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //edit category
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj); //update category obj to category table using entity framework
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitOfWork.Category.GetFirstOrDefault(u => u.CategoryId == id);
            if (obj == null)
            {
                NotFound();
            }
            else
            {
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
