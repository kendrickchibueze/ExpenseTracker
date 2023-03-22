using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExpenseTracker.BLL.Models;
using ExpenseTracker.DAL.Data;
using ExpenseTracker.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.MVC.Controllers
{
    public class CategoryController : Controller
    {


        private readonly ExpenseTrackerDbContext _context;
        private readonly IMapper _mapper;




        public CategoryController(IMapper mapper, ExpenseTrackerDbContext context)
        {
            _context = context;
            _mapper = mapper;   

        }

     

        public async Task<IActionResult> Index()
        {
            var categories = _context.Categories.ToList();
            var categoryVMs = _mapper.Map<List<CategoryVM>>(categories);

            return View(categoryVMs);
        }





     

        public IActionResult AddOrEdit(int id = 0)
        {
            CategoryVM categoryVM;

            if (id == 0)
            {
                categoryVM = new CategoryVM();
            }
            else
            {
                var category = _context.Categories.Find(id);

                if (category == null)
                {
                    return NotFound();
                }

                categoryVM = _mapper.Map<CategoryVM>(category);
            }

            return View(categoryVM);
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] CategoryVM categoryVM)
        {
            if (ModelState.IsValid)
            {
                Category category;

                if (categoryVM.CategoryId == 0)
                {
                    category = new Category();
                }
                else
                {
                    category = await _context.Categories.FindAsync(categoryVM.CategoryId);
                }

                if (category == null)
                {
                    return NotFound();
                }
                category.CategoryId = categoryVM.CategoryId;
                category.Title = categoryVM.Title;
                category.Icon = categoryVM.Icon;
                category.Type = categoryVM.Type;

                if (categoryVM.CategoryId == 0)
                {
                    _context.Categories.Add(category);
                }

                await _context.SaveChangesAsync();

                var categories = await _context.Categories.Select(c => new CategoryVM
                {
                    CategoryId = c.CategoryId,
                    Title = c.Title,
                    Icon = c.Icon,
                    Type = c.Type
                }).ToListAsync();

                return View("Index", categories);
            }

            return View(categoryVM);
        }










        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }




    }
}
