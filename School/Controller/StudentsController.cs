using School.Services;
using SchoolDomain;

namespace School.Controller
{
    public class StudentsController
    {
 //       private readonly StudentRepository studentRepository;
/*        private StudentsController(StudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }*/
        public  async Task AddStudent(Student student)
        {
            try
            {
                StudentRepository studentRepository =  new StudentRepository();
                await studentRepository.AddStudent(student);
                Console.WriteLine("Student is added");
            }
            catch (Exception)
            {

                Console.WriteLine("An error occurred while adding a student, please call us");
            }
        }
        public async Task AddCourse(int studentId, Course course)
        {
            try
            {
                StudentRepository studentRepository = new StudentRepository();
                await studentRepository.AddCourseOfStudent(studentId, course);
                Console.WriteLine("Course is added");
            }
            catch (Exception)
            {

                Console.WriteLine("An error occurred while adding a Course, please call us");
            }
        }
        public async Task GetStudets()
        {
            try
            {
                StudentRepository studentRepository = new StudentRepository();
                var students = await studentRepository.GetStudents();
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine(student.Name);
                        var courses = student.Courses;
                        if (courses != null)
                            foreach (var course in courses)
                                Console.WriteLine(course.Name + "\n" + course.Description);

                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("An error occurred while Getting a students, please call us");
            }
        }
    }

}
