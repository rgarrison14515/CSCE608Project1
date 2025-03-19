using System.ComponentModel.DataAnnotations;

namespace StudentRecordsAPI.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        public ICollection<Faculty>? FacultyMembers { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Major>? Majors { get; set; }
    }
}
