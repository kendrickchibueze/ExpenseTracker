using AutoMapper;
using ExpenseTracker.BLL.Models;
using ExpenseTracker.DAL.Data;
using ExpenseTracker.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.MVC.Controllers
{
    public class TransactionController : Controller
    {
      
            private readonly ExpenseTrackerDbContext _context;
            private readonly IMapper _mapper;

        public TransactionController(IMapper mapper, ExpenseTrackerDbContext context)
            {
                _context = context;
                  _mapper = mapper;
            }

        // GET: Transaction
        /*  public async Task<IActionResult> Index()
          {
              var applicationDbContext = _context.Transactions.Include(t => t.Category);
              return View(await applicationDbContext.ToListAsync());
          }
*/




        /*  public async Task<IActionResult> Index()
          {
              var transactions = await _context.Transactions
                  .Include(t => t.Category)
                  .ToListAsync();

              var transactionVMs = transactions.Select(t => new TransactionVM
              {
                  TransactionId = t.TransactionId,
                  CategoryId = t.CategoryId,
                  Category = new CategoryVM
                  {
                      CategoryId = t.Category.CategoryId,
                      Title = t.Category.Title,
                      Icon = t.Category.Icon,
                      Type = t.Category.Type
                  },
                  Amount = t.Amount,
                  Note = t.Note,
                  Date = t.Date
              });

              return View(transactionVMs);
          }*/


        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions.Include(t => t.Category).ToListAsync();
            var transactionVMs = _mapper.Map<IEnumerable<TransactionVM>>(transactions);
            return View(transactionVMs);
        }





























        // GET: Transaction/AddOrEdit
        /*        public IActionResult AddOrEdit(int id = 0)
                    {
                        PopulateCategories();
                        if (id == 0)
                            return View(new Transaction());
                        else
                            return View(_context.Transactions.Find(id));
                    }*/


        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCategories();

            if (id == 0)
            {
                return View(new TransactionVM());
            }
            else
            {
                var transaction = _context.Transactions.Find(id);

                if (transaction == null)
                {
                    return NotFound();
                }

                var transactionVM = _mapper.Map<TransactionVM>(transaction);
                return View(transactionVM);
            }
        }






        // POST: Transaction/AddOrEdit

        /*  [HttpPost]
              [ValidateAntiForgeryToken]
              public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] TransactionVM transaction)
              {
                  if (ModelState.IsValid)
                  {
                      if (transaction.TransactionId == 0)
                          _context.Add(transaction);
                      else
                          _context.Update(transaction);
                      await _context.SaveChangesAsync();
                      return RedirectToAction(nameof(Index));
                  }
                  PopulateCategories();
                  return View(transaction);
              }
  */



        /*
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] TransactionVM transactionVM)
                {
                    if (ModelState.IsValid)
                    {
                        var transaction = _mapper.Map<Transaction>(transactionVM);

                        if (transactionVM.TransactionId == 0)
                            _context.Add(transaction);
                        else
                            _context.Update(transaction);

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    PopulateCategories();
                    return View(transactionVM);
                }
        */

        /*
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> AddOrEdit(TransactionVM transactionVM)
                {
                    if (ModelState.IsValid)
                    {
                        var transaction = _mapper.Map<Transaction>(transactionVM);

                        if (transactionVM.TransactionId == 0)
                            _context.Add(transaction);
                        else
                            _context.Update(transaction);

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    PopulateCategories();
                    return View(transactionVM);
                }*/









        /*   [HttpPost]
           [ValidateAntiForgeryToken]
           public async Task<IActionResult> AddOrEdit(TransactionVM transactionVM)
           {
               if (ModelState.IsValid)
               {
                   var transaction = _mapper.Map<Transaction>(transactionVM);

                   // Set Category property to null if CategoryId is not set
                   if (transaction.CategoryId == 0)
                       transaction.Category = null;

                   if (transactionVM.TransactionId == 0)
                       _context.Add(transaction);
                   else
                       _context.Update(transaction);

                   await _context.SaveChangesAsync();
                   return RedirectToAction(nameof(Index));
               }

               PopulateCategories();
               return View(transactionVM);
           }
   */

        /*    [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddOrEdit(TransactionVM transactionVM)
            {
                if (ModelState.IsValid)
                {
                    var transaction = _mapper.Map<Transaction>(transactionVM);

                    // Check if CategoryId is valid
                    if (transaction.CategoryId != 0)
                    {
                        var category = await _context.Categories.FindAsync(transaction.CategoryId);
                        if (category == null)
                        {
                            // If CategoryId is not valid, return an error
                            ModelState.AddModelError(nameof(transactionVM.CategoryId), "Invalid category selected.");
                            PopulateCategories();
                            return View(transactionVM);
                        }
                    }
                    else
                    {
                        // If CategoryId is not set, set Category property to null
                        transaction.Category = null;
                    }

                    if (transactionVM.TransactionId == 0)
                        _context.Add(transaction);
                    else
                        _context.Update(transaction);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                PopulateCategories();
                return View(transactionVM);
            }
    */



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(TransactionVM transactionVM)
        {
            if (ModelState.IsValid)
            {
                var transaction = _mapper.Map<Transaction>(transactionVM);

                // Check if CategoryId is valid
                if (transaction.CategoryId != 0)
                {
                    var category = await _context.Categories.FindAsync(transaction.CategoryId);
                    if (category == null)
                    {
                        // If CategoryId is not valid, return an error
                        ModelState.AddModelError(nameof(transactionVM.CategoryId), "Invalid category selected.");
                        PopulateCategories();
                        return View(transactionVM);
                    }
                    transaction.Category = category; // Set the Category property of the Transaction object
                }
                else
                {
                    // If CategoryId is not set, set Category property to null
                    transaction.Category = null;
                }

                if (transactionVM.TransactionId == 0)
                    _context.Add(transaction);
                else
                    _context.Update(transaction);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateCategories();
            return View(transactionVM);
        }
















        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Transactions == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
                }
                var transaction = await _context.Transactions.FindAsync(id);
                if (transaction != null)
                {
                    _context.Transactions.Remove(transaction);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            [NonAction]
            public void PopulateCategories()
            {
                var CategoryCollection = _context.Categories.ToList();
                Category DefaultCategory = new Category() { CategoryId = 0, Title = "Choose a Category" };
                CategoryCollection.Insert(0, DefaultCategory);
                ViewBag.Categories = CategoryCollection;
            }
        }
    }

