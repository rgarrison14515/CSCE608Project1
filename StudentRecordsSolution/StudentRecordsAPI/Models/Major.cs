using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    [Table("Major")] //  Explicitly map to "Major" table
    public class Major
    {
        [Key]
        public int MajorID { get; set; }

        [Required]
        [StringLength(255)] //  Matches SQL's VARCHAR(255)
        public string Name { get; set; }

        public int? DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }

        public ICollection<StudentMajor>? StudentMajors { get; set; }
    }
}
