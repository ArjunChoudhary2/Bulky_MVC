using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bulky_Razor.Data;
using Bulky_Razor.Models;

namespace Bulky_Razor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        public readonly ApplicationDbContext _dbContext;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _dbContext.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            Category? obj = _dbContext.Categories.Find(Category.CategoryId);
            if (obj == null)
            {
                NotFound();
            }
            else
            {
                _dbContext.Categories.Remove(obj);
                _dbContext.SaveChanges();
                TempData["success"] = "Category deleted successfully";
            }

            return RedirectToPage("Index");

        }
    }
}
