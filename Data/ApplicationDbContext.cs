
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models.Entity;

namespace StudentManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students {  get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
