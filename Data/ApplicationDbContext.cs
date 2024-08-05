
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models.Entity;

namespace StudentManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students {  get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentCourse>().HasKey(sc => new {sc.StudentId, sc.CourseId});

            modelBuilder.Entity<StudentCourse>()
                .HasOne(s => s.student)
                .WithMany(sc => sc.studentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(c => c.course)
                .WithMany(sc => sc.studentCourses)
                .HasForeignKey(c => c.CourseId);

            //Account - Staff
            // Configure one-to-one relationship between Account and Staff
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Staff)
                .WithOne(s => s.Account)
                .HasForeignKey<Staff>(s => s.AccountId);
        }
    }
}
