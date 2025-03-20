namespace StudentRecordsAPI.Models.DTOs
{
    public class StudentWithMajorsDto
    {
        public string StudentID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Majors { get; set; }
    }
}
