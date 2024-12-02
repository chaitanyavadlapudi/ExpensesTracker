using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Models
    
{
    public class ExpenseTrackerDbcontext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public ExpenseTrackerDbcontext(DbContextOptions<ExpenseTrackerDbcontext> options)
            :base(options)
        {
            
        }
    }
}
