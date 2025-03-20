using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    [Table("StudentMajor")] // 👈 Explicitly map to the StudentMajor table
    public class StudentMajor
    {
        [Key]
        [Column(Order = 0)] // 👈 Composite Primary Key (StudentID, MajorID)
        public string StudentID { get; set; }

        [Key]
        [Column(Order = 1)] // 👈 Composite Primary Key (StudentID, MajorID)
        public int MajorID { get; set; }

        [ForeignKey("StudentID")]
        public Student? Student { get; set; }

        [ForeignKey("MajorID")]
        public Major? Major { get; set; }
    }
}
