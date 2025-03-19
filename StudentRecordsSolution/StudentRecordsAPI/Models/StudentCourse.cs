using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    public class StudentCourse
    {
        public string StudentID { get; set; }
        public int CourseID { get; set; }

        [ForeignKey("StudentID")]
        public Student? Student { get; set; }

        [ForeignKey("CourseID")]
        public Course? Course { get; set; }
    }
}
