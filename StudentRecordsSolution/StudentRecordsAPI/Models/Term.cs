using System.ComponentModel.DataAnnotations;

namespace StudentRecordsAPI.Models
{
    public class Term
    {
        [Key]
        public int TermID { get; set; }

        [Required]
        public string TermCode { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
