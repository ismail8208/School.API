namespace SchoolDomain
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public int StudentId { get; set; }
        public Student student { get; set; }
        //public IList<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
