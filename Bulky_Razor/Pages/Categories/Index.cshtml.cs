using Bulky_Razor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bulky_Razor.Models;

namespace Bulky_Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public List<Category> CategoryList { get; set; } 

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
            CategoryList = _dbContext.Categories.ToList();
        }
    }
}
