using DataAccess;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using School.API.ViewModels;
using SchoolDomain;

namespace School.API.Services
{
    public class StudentRepository : IStudentRepository
    {
        // you can't use injection syntax without added the service to your app (builder)
        private readonly SchoolContext context;
        public StudentRepository(SchoolContext context)
        {
            this.context = context;
        }

        public async Task<(List<Student>, PaginationMetaData)> GetStudents(int pageSize,int pageNumber,string name)
        {
            int totalStudentCount = await context.Students.CountAsync();
            var paginationMetaData = new PaginationMetaData(pageSize,pageNumber, totalStudentCount);
            var query = context.Students as IQueryable<Student>;
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(s =>s.Name.Contains(name));
            }
            var filtered = await
             query
            .Where(s => s.IsDeleted == false)
            .OrderBy(s =>s.Name)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            /*.Include(c => c.Courses)*/
            .ToListAsync();
            return (filtered,paginationMetaData);
        }
        public async Task<Student?> GetStudent(int studentId)
        {
                return await context.Students.Where(s => s.StudentId == studentId && s.IsDeleted == false).FirstOrDefaultAsync();
        }
        /*public async Task<List<Course>> GetCoursesOfStudent(int studentId)
        {
                return await context.Courses.Where(c => c.StudentCourses == studentId && c.IsDeleted == false).ToListAsync();
        }*/

       /* public async Task<Course?> GetCourseOfStudent(int studentId, int courseId)
        {
                return await context.Courses.Where(c => c.StudentId == studentId && c.CourseId == courseId && c.IsDeleted == false).FirstOrDefaultAsync();
        }*/
        public async Task AddStudent(Student student)
        {
                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();
        }

        /*public async Task AddCourseOfStudent(int studentId, Course course)
        {
                course.StudentId = studentId;
                await context.Courses.AddAsync(course);
                await context.SaveChangesAsync();
        }*/
        public async Task DeleteStudent(int studentId)
        {
                var student = await context.Students.Where(s => s.StudentId == studentId && s.IsDeleted == false).FirstOrDefaultAsync();
                student.IsDeleted = true;
                context.Students.Update(student);
                await context.SaveChangesAsync();
        }
        /*public async Task DeleteCourseOfStudent(int studentId, int courseId)
        {
                var course = await context.Courses.Where(c => c.CourseId == courseId && c.StudentId == studentId && c.IsDeleted == false).FirstOrDefaultAsync();
                course.IsDeleted = true;
                context.Courses.Update(course);
                await context.SaveChangesAsync();
        }*/

        public async Task UpdateStudent(int studentId, StudentForUpdate studentForUpdate)
        {
                var student = await context.Students.Where(s => s.StudentId == studentId && s.IsDeleted == false).FirstOrDefaultAsync();
                student.Name = studentForUpdate.Name;
                context.Students.Update(student);
                await context.SaveChangesAsync();
        }
        public async Task PartiallyUpdateStudent(int studentId, JsonPatchDocument<StudentForUpdate> patchDocument)
        {
                var student = await context.Students.Where(s => s.StudentId == studentId && s.IsDeleted == false).FirstOrDefaultAsync();
                var brandToBePatch = new StudentForUpdate()
                {
                    Name = student.Name,
                };

                patchDocument.ApplyTo(brandToBePatch);
                student.Name = brandToBePatch.Name;

                context.Students.Update(student);
                await context.SaveChangesAsync();
        }
    }
}
