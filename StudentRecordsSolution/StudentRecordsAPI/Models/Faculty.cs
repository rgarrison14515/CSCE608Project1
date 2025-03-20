using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    [Table("Faculty")] // 👈 Explicitly map to "Faculty" table
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }

        [Required]
        [StringLength(255)] // 👈 Matches SQL's VARCHAR(255)
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)] // 👈 Matches SQL's VARCHAR(255) for email
        public string Email { get; set; }

        public int? DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
