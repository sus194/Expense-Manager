using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Expense_Manager.Models;

namespace Expense_Manager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Expense_Manager.Models.Expense>? Expense { get; set; }
        public DbSet<Expense_Manager.Models.ExpenseLimit>? ExpenseLimit { get; set; }
        
    }
}