using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolDomain;

namespace DataAccess
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        // public DbSet<StudentCourse> StudentCourses { get; set; }
/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SchoolDatabase2").
                LogTo(e => Console.WriteLine(e), new[] { DbLoggerCategory.Database.Name }, LogLevel.Information).EnableSensitiveDataLogging();
        }*/

        /*    protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });

             modelBuilder.Entity<StudentCourse>()
                 .HasOne<Student>(s => s.student)
                 .WithMany(sc => sc.StudentCourses)
                 .HasForeignKey(sc => sc.StudentId);
             modelBuilder.Entity<StudentCourse>()
                 .HasOne<Course>(c => c.course)
                 .WithMany(sc => sc.StudentCourses)
                 .HasForeignKey(c => c.CourseId);
         }*/
        //seeding default student
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    StudentId = 1,
                    Name = "First Student",

                },                new Student()
                {
                    StudentId = 2,
                    Name = "Second Student",

                },                new Student()
                {
                    StudentId = 3,
                    Name = "Third Student",

                },                new Student()
                {
                    StudentId = 4,
                    Name = "Fourth Student",

                },                new Student()
                {
                    StudentId = 5,
                    Name = "Fifth Student",

                }
                );
            modelBuilder.Entity<Course>().HasData(
                new Course()
                {
                    CourseId = 1,
                    Name = " First Course",
                    Description = " My for first student",
                    StudentId = 1,
                },
                 new Course()
                 {
                     CourseId = 2,
                     Name = " Second Course",
                     Description = " alSO I for first student",
                     StudentId = 1,
                 }
                );
        }
    }
}