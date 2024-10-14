using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        
        public readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            return View(companies);
        }

        public ActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                //Create
                return View(new Company());
            }
            else
            {
                //Update
                Company company = _unitOfWork.Company.GetFirstOrDefault(u => u.CompanyId == id);
                return View(company);
            }
        }
        [HttpPost]
        public ActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.CompanyId == 0)
                {
                    // Add new product
                    _unitOfWork.Company.Add(obj);
                }
                else
                {
                    // Update existing product
                    _unitOfWork.Company.Update(obj);

                }

                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        #region Api Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = companies });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
           var companyToBeDeleted = _unitOfWork.Company.GetFirstOrDefault(u => u.CompanyId == id);
            if(companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting " });
            }
            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted Successfully " });
        }
        }

        #endregion
    }

