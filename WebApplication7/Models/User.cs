using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication7.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        [NotMapped]
        public string EmailConfirmed { get; set; } = string.Empty;
        public string LinkedIn { get; set; }
        public string PreferredTrack { get; set; }
        public string Major { get; set; }
        public string University { get; set; }
        public int GraduationYear { get; set; }
        public ICollection<Resume> Resumes { get; set; }
    }
}

