using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Manager.Data;
using Expense_Manager.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Expense_Manager.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;



        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenses
        [Authorize]
        public async Task<IActionResult> Index()
        {
            
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _context.Expense.ToListAsync();
            

            // Filter expenses for the logged-in user
            var userExpenses = expenses.Where(e => e.ExpenseUserId == userId).ToList();

            return View(userExpenses);
        }

        // GET: Expenses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Expense == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        [Authorize]
        public IActionResult Create()
        {
            var distinctExpenseTypes = _context.Expense
                .Select(e => e.ExpenseType)
                .Distinct().ToList();

            var additionalExpenseTypes = new List<string>
            {
                "Operating",
                "Non-operating",
                "Fixed",
                "Variable",

            };

            distinctExpenseTypes = distinctExpenseTypes.Union(additionalExpenseTypes).ToList();

            TempData["Typeslist"] = distinctExpenseTypes;
            return View();
        }


        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,ExpenseName,ExpenseType,ExpenseAmount,ExpenseDate, ExpenseUserId")] Expense expense)
        {
           
            Console.WriteLine(expense.ExpenseType);
            if (ModelState.IsValid)
            {

                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                expense.ExpenseUserId = userId;
                _context.Add(expense);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Expense added";
                return RedirectToAction(nameof(Create));
            }
            
            return View(expense);
        }

        // GET: Expenses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Expense == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            var distinctExpenseTypes = _context.Expense
                .Select(e => e.ExpenseType)
                .Distinct().ToList();
            var additionalExpenseTypes = new List<string>
            {
                "Operating",
                "Non-operating",
                "Fixed",
                "Variable",

            };

            distinctExpenseTypes = distinctExpenseTypes.Union(additionalExpenseTypes).ToList();

            TempData["Typeslist"] = distinctExpenseTypes;
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpenseName,ExpenseType,ExpenseAmount,ExpenseDate,ExpenseUserId")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Search));
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Expense == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Expense == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Expense'  is null.");
            }
            var expense = await _context.Expense.FindAsync(id);
            if (expense != null)
            {
                _context.Expense.Remove(expense);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Search));
        }
        [HttpPost]
        public async Task<IActionResult> Search(String Name, String Type, int AmountFrom, int AmountTo, DateTime DateFrom, DateTime DateTo)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _context.Expense.ToListAsync();

            // Filter expenses for the logged-in user
            var userExpenses = expenses.Where(e => e.ExpenseUserId == userId).ToList();

            if(Name != null)
            {
                userExpenses = userExpenses.Where(e => e.ExpenseName == Name).ToList();
            }

            if(Type != null)
            {
                userExpenses = userExpenses.Where(e => e.ExpenseType == Type).ToList();
            }

            if(AmountFrom != 0)
            {
                if(AmountTo != 0)
                {
                    userExpenses = userExpenses.Where(e => e.ExpenseAmount >= AmountFrom && e.ExpenseAmount <= AmountTo).ToList();
                }
                else
                {
                    userExpenses = userExpenses.Where(e => e.ExpenseAmount >= AmountFrom).ToList();
                }
            }

            else if(AmountTo != 0)
            {
                userExpenses = userExpenses.Where(e => e.ExpenseAmount <= AmountTo).ToList();
            }

            if(DateTime.Compare(DateFrom, DateTo) < 0)
            {
                userExpenses = userExpenses.Where(e => e.ExpenseDate >= DateFrom && e.ExpenseDate <= DateTo).ToList();
            }

            Response.Cookies.Append("SearchParameters", $"{Name}|{Type}|{AmountFrom}|{AmountTo}|{DateFrom}|{DateTo}");

            return View(userExpenses);

        }

        public async Task<IActionResult> Search()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _context.Expense.ToListAsync();

            // Filter expenses for the logged-in user
            var userExpenses = expenses.Where(e => e.ExpenseUserId == userId).ToList();

            // Retrieve the search parameters from the cookie
            var searchParameters = Request.Cookies["SearchParameters"];

            if (!string.IsNullOrEmpty(searchParameters))
            {
                var parameters = searchParameters.Split('|');

                string Name = parameters[0];
                string Type = parameters[1];
                int AmountFrom = int.Parse(parameters[2]);
                int AmountTo = int.Parse(parameters[3]);
                DateTime DateFrom = DateTime.Parse(parameters[4]);
                DateTime DateTo = DateTime.Parse(parameters[5]);

                if (!string.IsNullOrEmpty(Name))
                {
                    userExpenses = userExpenses.Where(e => e.ExpenseName == Name).ToList();
                }

                if (!string.IsNullOrEmpty(Type))
                {
                    userExpenses = userExpenses.Where(e => e.ExpenseType == Type).ToList();
                }

                if (AmountFrom != 0)
                {
                    if (AmountTo != 0)
                    {
                        userExpenses = userExpenses.Where(e => e.ExpenseAmount >= AmountFrom && e.ExpenseAmount <= AmountTo).ToList();
                    }
                    else
                    {
                        userExpenses = userExpenses.Where(e => e.ExpenseAmount >= AmountFrom).ToList();
                    }
                }
                else if (AmountTo != 0)
                {
                    userExpenses = userExpenses.Where(e => e.ExpenseAmount <= AmountTo).ToList();
                }

                if (DateTime.Compare(DateFrom, DateTo) < 0)
                {
                    userExpenses = userExpenses.Where(e => e.ExpenseDate >= DateFrom && e.ExpenseDate <= DateTo).ToList();
                }
            }

            return View(userExpenses);
        }




        [Authorize]
        //GET
        public async Task<IActionResult> ExpenseLimit()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var limit = await _context.ExpenseLimit.ToListAsync();

            // Filter expenses for the logged-in user
            var userExpenses = limit.Where(e => e.ExpenseUserId == userId);

            return View();
        }


        private bool ExpenseExists(int id)
        {
          return (_context.Expense?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
