using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    public class StudentMajor
    {
        public string StudentID { get; set; }
        public int MajorID { get; set; }

        [ForeignKey("StudentID")]
        public Student? Student { get; set; }

        [ForeignKey("MajorID")]
        public Major? Major { get; set; }
    }
}
