using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentRecordsAPI.Models
{
    [Table("Major")]
    public class Major
    {
        [Key]
        public int MajorID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int? DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Department? Department { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<StudentMajor>? StudentMajors { get; set; }
    }
}
