using Microsoft.EntityFrameworkCore;
using NMCNPM.Models;  // Tham chiếu đến Models nếu cần

namespace NMCNPM.Data  // Đảm bảo đúng namespace
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
    }
}