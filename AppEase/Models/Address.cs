using System.ComponentModel.DataAnnotations;

namespace AppEase.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Street Address required")]
        public string Street { get; set; }
        public string? Apartment {  get; set; }
        [Required(ErrorMessage = "City required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State required")]
        public string State { get; set; }
        [Required(ErrorMessage = "Country required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Pincode required")]
        public int Pincode { get; set; }

    }
}
