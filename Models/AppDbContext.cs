using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoPhoenix.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // We'll add these later
        // public DbSet<Project> Projects { get; set; }
        // public DbSet<TaskItem> Tasks { get; set; }
    }
}