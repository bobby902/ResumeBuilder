namespace WebApplication7.Models
{
    public class Education
    {

        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string SchoolName { get; set; }

        public string FieldOfStudy { get; set; }
        public int GraduationYear { get; set; }

        public Resume Resume { get; set; }
    }
}
