using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Models
{
    public class ExpenseContext(DbContextOptions<ExpenseContext> options) : DbContext(options)
    {
        public DbSet<Expense> Expenses { get; set; } = null!;
    }
}
