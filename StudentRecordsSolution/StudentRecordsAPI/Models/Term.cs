using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; //  Import this namespace

namespace StudentRecordsAPI.Models
{
    [Table("Term")] // Explicitly maps to the Term table
    public class Term
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensures IDENTITY property in SQL
        public int TermID { get; set; }

        [Required]
        [StringLength(20)] // Matches SQL: TermCode VARCHAR(20) UNIQUE NOT NULL
        public string TermCode { get; set; }

        //  Prevents serialization if it's null
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Course>? Courses { get; set; }
    }
}
