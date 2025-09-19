using FinanceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Data
{
    public class ExpenseContext(DbContextOptions<ExpenseContext> options) : DbContext(options)
    {
        public DbSet<Expense> Expenses { get; set; } = null!;
    }
}
