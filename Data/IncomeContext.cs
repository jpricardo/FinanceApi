using FinanceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Data
{
    public class IncomeContext(DbContextOptions<IncomeContext> options) : DbContext(options)
    {
        public DbSet<Income> Incomes { get; set; } = null!;
    }
}
