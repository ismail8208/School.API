using SchoolDomain;
using System.Runtime.CompilerServices;

namespace School.Services
{
    internal interface IStudentRepository
    {
        public Task<List<Student>> GetStudents();
        public Task<Student?> GetStudent(int studentId);
        public Task<List<Course>> GetCoursesOfStudent(int studentId);
        public Task<Course?> GetCourseOfStudent(int studentId, int courseId);
        public Task AddStudent(Student student);
        public Task AddCourseOfStudent(int studentId,Course course);
        public Task DeleteStudent(int studentId);
        public Task DeleteCourseOfStudent(int studentId, int courseId);

    }
}
