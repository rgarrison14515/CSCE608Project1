using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentRecordsAPI.Models
{
    [Table("Faculty")]
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        public int? DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Department? Department { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Course>? Courses { get; set; }
    }
}
