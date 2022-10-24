using Microsoft.AspNetCore.JsonPatch;
using School.API.ViewModels;
using SchoolDomain;

namespace School.API.Services
{
    public interface IStudentRepository
    {
        public Task<(List<Student>, PaginationMetaData)> GetStudents(int pageSize,int pageNumber,string name);
        public Task<Student?> GetStudent(int studentId);
        //public Task<List<Course>> GetCoursesOfStudent(int studentId);
       // public Task<Course?> GetCourseOfStudent(int studentId, int courseId);
        public Task AddStudent(Student student);
        //public Task AddCourseOfStudent(int studentId,Course course);
        public Task DeleteStudent(int studentId);
       // public Task DeleteCourseOfStudent(int studentId, int courseId);
        public Task UpdateStudent(int studentId, StudentForUpdate student);
        public Task PartiallyUpdateStudent(int studentId, JsonPatchDocument<StudentForUpdate> patchDocument);

    }
}
