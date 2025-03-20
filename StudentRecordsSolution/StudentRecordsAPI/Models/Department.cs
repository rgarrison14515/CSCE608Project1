using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    [Table("Department")] // 👈 Explicitly map to "Department" table
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(10)] // 👈 Ensures abbreviation follows the VARCHAR(10) limit in SQL
        public string Abbreviation { get; set; }

        public ICollection<Faculty>? FacultyMembers { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Major>? Majors { get; set; }
    }
}
