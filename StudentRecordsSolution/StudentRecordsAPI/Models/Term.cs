using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRecordsAPI.Models
{
    [Table("Term")] // 👈 Explicitly maps to the Term table
    public class Term
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 👈 Ensures IDENTITY property in SQL
        public int TermID { get; set; }

        [Required]
        [StringLength(20)] // 👈 Matches SQL: TermCode VARCHAR(20) UNIQUE NOT NULL
        public string TermCode { get; set; }

        // Navigation Property
        public ICollection<Course>? Courses { get; set; }
    }
}
