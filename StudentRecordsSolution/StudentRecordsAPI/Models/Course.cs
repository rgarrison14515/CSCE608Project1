using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Credits { get; set; }

        public int? DepartmentID { get; set; }
        public int? FacultyID { get; set; }
        public int? TermID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }

        [ForeignKey("FacultyID")]
        public Faculty? Faculty { get; set; }

        [ForeignKey("TermID")]
        public Term? Term { get; set; }

        public ICollection<StudentCourse>? StudentCourses { get; set; }
    }
}
