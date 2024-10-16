namespace WebApplication7.Models
{
    public class Experience
    {


        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Responsibilities { get; set; }

        public Resume Resume { get; set; }
    }
}
