using AutoMapper;
using SchoolDomain;

namespace School.API.ViewModels
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student,StudentWithoutCourses>();
        }
    }
}
