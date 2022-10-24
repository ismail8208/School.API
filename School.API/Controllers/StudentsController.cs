using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using School.API.Services;
using School.API.ViewModels;
using SchoolDomain;
using System.ComponentModel.DataAnnotations;
using System.Xml.XPath;

namespace School.API.Controller
{
    [ApiController]
    [Route(@"api/[controller]")]
    [Authorize(policy:"ShouldBeUser")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> logger;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private int maxSize = 10;
        public StudentsController(ILogger<StudentsController> logger, IStudentRepository studentRepository,IMapper mapper)
        {
            this.logger = logger;
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        //Undocumented
        // public async Task<ActionResult<List<Student>>> GetStudents()
        // Error: response status is 406
        public async Task<ActionResult<List<Student>>> GetStudents([FromQuery]int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] string name="")
        {
            //logger.LogInformation("hi my Get student endpoint ...............");
            if (pageSize > maxSize) pageSize = maxSize;

            var (students,paginationMetaData) = await studentRepository.GetStudents(pageSize, pageNumber,name);
            if (students == null)
            {
                return NotFound();
            }
            else
            {
                Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetaData));
                return Ok(mapper.Map<List<StudentWithoutCourses>>(students));
            }
        }

        [HttpGet("{Studentid}", Name = "GetStudent")]
        public async Task<ActionResult<Student>> GetStudent(int Studentid)
        {
            var student = await studentRepository.GetStudent(Studentid);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(student);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(StudentForCreation student)
        {
            // if we haven't database we add in this way 
            /*
             var LastExsitingStudent = studentRepository.GetStudent().Result.Max(p => p.StudentId);
             var studentToBeAdded = new Student()
            { 
                StudentId = ++LastExistingStudent,
                Name = student.Name,
                IsDeleted = false,
            };
             */
            var studentToBeAdded = new Student()
            {
                Name = student.Name,
            };
            await studentRepository.AddStudent(studentToBeAdded);
            return CreatedAtRoute("GetStudent", new
            {
                // should be 'Studentid' because the GetStudent EndPoint has one parameter name is StudentId 
                //        [HttpGet("{Studentid}", Name = "GetStudent")]
                Studentid = studentToBeAdded.StudentId,
            }, studentToBeAdded);
        }

        [HttpPut(@"{studentId}")]
        public async Task<ActionResult> UpdateStudent(int studentId, StudentForUpdate student)
        {
            await studentRepository.UpdateStudent(studentId, student);
            return NoContent();
        }

        [HttpPatch(@"{studentId}")]
        public async Task<ActionResult> partiallyUpdateStudent(int studentId, JsonPatchDocument<StudentForUpdate> patchDocument)
        {
            await studentRepository.PartiallyUpdateStudent(studentId, patchDocument);
            return NoContent();
        }

        [HttpDelete(@"{studentId}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            await studentRepository.DeleteStudent(studentId);
            return NoContent();
        }
    }

}


/*
 private readonly StudentRepository studentRepository;
private StudentsController(StudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
public async Task AddStudent(Student student)
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

 */