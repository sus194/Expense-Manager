using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Manager.Data;
using Expense_Manager.Models;
using Microsoft.AspNetCore.Authorization;
namespace Expense_Manager.Controllers
{
    public class ExpenseLimitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseLimitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExpenseLimits
        /*
        public async Task<IActionResult> Index()
        {
              return _context.ExpenseLimit != null ? 
                          View(await _context.ExpenseLimit.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ExpenseLimit'  is null.");
        }

        // GET: ExpenseLimits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExpenseLimit == null)
            {
                return NotFound();
            }

            var expenseLimit = await _context.ExpenseLimit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseLimit == null)
            {
                return NotFound();
            }

            return View(expenseLimit);
        }

        */

        // GET: ExpenseLimits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseLimits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpenseType,Limit")] ExpenseLimit expenseLimit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenseLimit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expenseLimit);
        }

        // GET: ExpenseLimits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExpenseLimit == null)
            {
                return NotFound();
            }

            var expenseLimit = await _context.ExpenseLimit.FindAsync(id);
            if (expenseLimit == null)
            {
                return NotFound();
            }
            return View(expenseLimit);
        }

        // POST: ExpenseLimits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpenseType,Limit")] ExpenseLimit expenseLimit)
        {
            if (id != expenseLimit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenseLimit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseLimitExists(expenseLimit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expenseLimit);
        }

        // GET: ExpenseLimits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpenseLimit == null)
            {
                return NotFound();
            }

            var expenseLimit = await _context.ExpenseLimit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenseLimit == null)
            {
                return NotFound();
            }

            return View(expenseLimit);
        }

        // POST: ExpenseLimits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExpenseLimit == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExpenseLimit'  is null.");
            }
            var expenseLimit = await _context.ExpenseLimit.FindAsync(id);
            if (expenseLimit != null)
            {
                _context.ExpenseLimit.Remove(expenseLimit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseLimitExists(int id)
        {
          return (_context.ExpenseLimit?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
