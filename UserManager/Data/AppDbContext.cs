using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using UserManager.Model;
using Microsoft.EntityFrameworkCore;

namespace UserManager.Data
{
    public class AppDbContext : IdentityDbContext<CustomUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
