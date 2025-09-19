using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Models
{
    public class IncomeContext(DbContextOptions<IncomeContext> options) : DbContext(options)
    {
        public DbSet<Income> Incomes { get; set; } = null!;
    }
}
