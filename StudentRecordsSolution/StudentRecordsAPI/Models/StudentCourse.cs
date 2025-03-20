using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentRecordsAPI.Models
{
    [Table("StudentCourse")]
    public class StudentCourse
    {
        [Key]
        [Column(Order = 0)]
        public string StudentID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int CourseID { get; set; }

        [ForeignKey("StudentID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Student? Student { get; set; }

        [ForeignKey("CourseID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Course? Course { get; set; }
    }
}
