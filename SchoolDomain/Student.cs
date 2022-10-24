namespace SchoolDomain
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public List<Course> Courses { get; set; } = new List<Course>();
        //public IList<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}