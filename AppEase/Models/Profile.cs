﻿using System.ComponentModel.DataAnnotations;

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
        public List<Job>? Jobs { set; get; }


    }
}
