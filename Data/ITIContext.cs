using Microsoft.EntityFrameworkCore;
using Demo1_Day2.Models;
namespace Demo1_Day2.Data
{
    public class ITIContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet <Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ITIContext()
        {
        }
        public ITIContext(DbContextOptions<ITIContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer("Server=MYPC\\MSSQLSERVER2022;Database=Mans3;integrated security=true;trustservercertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>(e =>
            {
                e.HasKey(ele => new { ele.StdId, ele.CrsId });
            });
            modelBuilder.Entity<Course>()
                        .HasIndex(c => c.CrsName)
                        .IsUnique();
            modelBuilder.Entity<Student>()
                       .HasIndex(c => c.Email)
                       .IsUnique();
            modelBuilder.Entity<Department>()
                       .HasIndex(c => c.DeptName)
                       .IsUnique();

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Student" }
                );
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
