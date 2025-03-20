using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentRecordsAPI.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Abbreviation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Faculty>? FacultyMembers { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Course>? Courses { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Major>? Majors { get; set; }
    }
}
