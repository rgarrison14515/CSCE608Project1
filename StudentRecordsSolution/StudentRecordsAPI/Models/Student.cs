using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentRecordsAPI.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public string StudentID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        public int? MajorID { get; set; }

        [ForeignKey("MajorID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Major? Major { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<StudentCourse>? StudentCourses { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<StudentMajor>? StudentMajors { get; set; }
    }
}
