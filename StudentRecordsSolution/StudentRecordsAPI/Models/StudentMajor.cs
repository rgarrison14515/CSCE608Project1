using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentRecordsAPI.Models
{
    [Table("StudentMajor")]
    public class StudentMajor
    {
        [Key]
        [Column(Order = 0)]
        public string StudentID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int MajorID { get; set; }

        [ForeignKey("StudentID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Student? Student { get; set; }

        [ForeignKey("MajorID")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Major? Major { get; set; }
    }
}
