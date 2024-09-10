using Bulky_Razor.Data;
using Bulky_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
        public readonly ApplicationDbContext _dbContext;
        [BindProperty]
        public Category? Category { get; set; }
        public EditModel(ApplicationDbContext dbContext)
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
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(Category); //update category obj to category table using entity framework
                _dbContext.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
