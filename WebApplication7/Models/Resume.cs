namespace WebApplication7.Models
{
    public class Resume
    {


        public int Id { get; set; }



        public int UserId { get; set; }
        public User User { get; set; }
        public string Summary { get; set; }
        public string Skills { get; set; }

        public ICollection<Experience> Experiences { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<Project> Projects { get; set; }

        public Resume()
        {
            Experiences = new List<Experience>();
            Educations = new List<Education>();
            Projects = new List<Project>();
        }
        public void AddExperience(Experience experience)
        {
            Experiences.Add(experience);
        }

        public void AddEducation(Education education)
        {
            Educations.Add(education);
        }

        public void AddProject(Project project)
        {
            Projects.Add(project);
        }
    }
}
