using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    public class Student
    {
        [Key]
        public string StudentID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public int? MajorID { get; set; }

        [ForeignKey("MajorID")]
        public Major? Major { get; set; }

        public ICollection<StudentCourse>? StudentCourses { get; set; }
        public ICollection<StudentMajor>? StudentMajors { get; set; }
    }
}
