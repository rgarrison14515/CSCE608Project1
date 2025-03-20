using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    [Table("StudentCourse")] //  Explicitly map to the StudentCourse table
    public class StudentCourse
    {
        [Key]
        [Column(Order = 0)] //  Composite Primary Key (StudentID, CourseID)
        public string StudentID { get; set; }

        [Key]
        [Column(Order = 1)] //  Composite Primary Key (StudentID, CourseID)
        public int CourseID { get; set; }

        [ForeignKey("StudentID")]
        public Student? Student { get; set; }

        [ForeignKey("CourseID")]
        public Course? Course { get; set; }
    }
}
