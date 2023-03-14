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



        public CategoryController(ExpenseTrackerDbContext context)
        {
            _context = context;

        }

        //GET:Category

        public async Task<IActionResult> Index()
        {
            return _context.Categories != null ? View(await _context.Categories.ToListAsync()) : Problem("Entity set 'ExpenseTrackerDbContext.Categories' is null");
        }


        //GET: Category/Details/5
        /*  public async Task<IActionResult> Details(int? id)
          {

              if(id== null ||   _context.Categories == null)
              {
                  return NotFound();
              }

              var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);  

              if(category == null)
              {
                  category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
              }
              return null;

          }



          public async Task<IActionResult> Create([Bind("CategoryId,Title,Icon,Type")] CategoryVM category)
          {
              if (ModelState.IsValid)
              {
                  if (category.CategoryId == 0)
                  {
                      _context.Add(category);
                      await _context.SaveChangesAsync();
                      return RedirectToAction(nameof(Index));
                  }
                  return View(category);
              }
              return null;
          }*/




        //GET: Category/AddOrEdit


        public IActionResult AddOrEdit(int id = 0)
        {


            if (id == 0)
            {
                return View(new CategoryVM());
            }
            else
            {
                return View(_context.Categories.Find(id));
            }
        }


        // POST: Category/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] CategoryVM category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    _context.Add(category);
                }

                else
                {
                    _context.Update(category);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        // POST: Category/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ExpenseTrackerDbContext.Categories' is null");
            }

            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
