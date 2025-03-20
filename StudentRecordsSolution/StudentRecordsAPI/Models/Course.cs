using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // Import JSON handling for serialization control

namespace StudentRecordsAPI.Models
{
    [Table("Course")] // Explicitly map to "Course" table
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

        // Ignore navigation properties when null
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("FacultyID")]
        public Faculty? Faculty { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [ForeignKey("TermID")]
        public Term? Term { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<StudentCourse>? StudentCourses { get; set; }
    }
}
