using System.ComponentModel.DataAnnotations;

namespace AppEase.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { set; get; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { set; get; }
        public string? LinkedIn { set; get; }
        public string? Github { set; get; }
        [Required(ErrorMessage ="Resume is Required")]
        public string Resume {  set; get; }
        [Required(ErrorMessage ="Skill Set is Required")]
        public string SkillSet { set; get; }
        [Display(Name = "New Job")]
        public Job? NewJob { set; get; }
        public virtual ICollection<Job>? Jobs { set; get; }
        [Required(ErrorMessage = " Address Required ")]
        public Address Address { set; get; }


    }
}
