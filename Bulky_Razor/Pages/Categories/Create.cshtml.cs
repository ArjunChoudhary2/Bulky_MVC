using Bulky_Razor.Data;
using Bulky_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        public readonly ApplicationDbContext _dbContext;
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _dbContext = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _dbContext.Add(Category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category updated successfully";
            return RedirectToPage("Index");
        }
    }
}
