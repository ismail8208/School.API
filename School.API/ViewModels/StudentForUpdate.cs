using System.ComponentModel.DataAnnotations;

namespace School.API.ViewModels
{
    public class StudentForUpdate
    {
        [Required(ErrorMessage = " I want to value for update student Name")]
        public string Name { get; set; }
    }
}
