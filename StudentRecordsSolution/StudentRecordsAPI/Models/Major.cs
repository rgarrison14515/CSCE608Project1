using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    public class Major
    {
        [Key]
        public int MajorID { get; set; }

        [Required]
        public string Name { get; set; }

        public int? DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }

        public ICollection<StudentMajor>? StudentMajors { get; set; }
    }
}
