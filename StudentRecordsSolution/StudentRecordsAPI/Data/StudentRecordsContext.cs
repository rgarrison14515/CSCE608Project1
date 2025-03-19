using Microsoft.EntityFrameworkCore;
using StudentRecordsAPI.Models;

namespace StudentRecordsAPI.Data
{
    public class StudentRecordsContext : DbContext
    {
        public StudentRecordsContext(DbContextOptions<StudentRecordsContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentMajor> StudentMajors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentID, sc.CourseID });
            modelBuilder.Entity<StudentMajor>().HasKey(sm => new { sm.StudentID, sm.MajorID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
