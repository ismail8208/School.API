using DataAccess;
using Microsoft.EntityFrameworkCore;
using SchoolDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Services
{
    public class StudentRepository : IStudentRepository
    {
        // you can't use injection syntax without added the service to your app (builder)
/*        private readonly SchoolContext context;
        public StudentRepository()
        {

        }
        public StudentRepository(SchoolContext context)
        {
            this.context = context;
        }*/

        public async Task<List<Student>> GetStudents()
        {
            using (SchoolContext context = new SchoolContext())
                return await context.Students.Where(s => s.IsDeleted == false).Include(c => c.Courses).ToListAsync();
        }
        public async Task<Student?> GetStudent(int studentId)
        {
            using (SchoolContext context = new SchoolContext())
                return await context.Students.Where(s => s.StudentId == studentId && s.IsDeleted == false).FirstOrDefaultAsync();
        }
        public async Task<List<Course>> GetCoursesOfStudent(int studentId)
        {
            using (SchoolContext context = new SchoolContext())
                return await context.Courses.Where(c => c.StudentId == studentId && c.IsDeleted == false).ToListAsync();
        }

        public async Task<Course?> GetCourseOfStudent(int studentId, int courseId)
        {
            using (SchoolContext context = new SchoolContext())
                return await context.Courses.Where(c => c.StudentId == studentId && c.CourseId == courseId && c.IsDeleted == false).FirstOrDefaultAsync();
        }
        public async Task AddStudent(Student student)
        {
            using (SchoolContext context = new SchoolContext())
            {
                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddCourseOfStudent(int studentId, Course course)
        {
            using (SchoolContext context = new SchoolContext())
            {
                course.StudentId = studentId;
                await context.Courses.AddAsync(course);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteStudent(int studentId)
        {
            using (SchoolContext context = new SchoolContext())
            {
                var student = await context.Students.Where(s => s.StudentId == studentId && s.IsDeleted == false).FirstOrDefaultAsync();
                student.IsDeleted = true;
                context.Students.Update(student);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteCourseOfStudent(int studentId, int courseId)
        {
            using (SchoolContext context = new SchoolContext())
            {
                var course = await context.Courses.Where(c => c.CourseId == courseId && c.StudentId == studentId && c.IsDeleted == false).FirstOrDefaultAsync();
                course.IsDeleted = true;
                context.Courses.Update(course);
                await context.SaveChangesAsync();
            }
        }
    }
}
