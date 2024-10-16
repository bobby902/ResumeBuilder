namespace WebApplication7.Models
{
    public class Project
    {

        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string ProjectTitle { get; set; }
        public string Description { get; set; }
        public string TechnologiesUsed { get; set; }

        public Resume Resume { get; set; }

    }
}
