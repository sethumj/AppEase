using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AppEase.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Job Title required")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Job Description Requied")]
        public string Description { get; set; }

    }
}
