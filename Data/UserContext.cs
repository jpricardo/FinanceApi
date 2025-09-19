using FinanceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Data
{
    public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
    }
}
