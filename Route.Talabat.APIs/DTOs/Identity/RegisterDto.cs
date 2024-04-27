using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = null!;
      
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;


        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [RegularExpression("^(?=(.*\\d){2})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\\d]).{8,}$",
            ErrorMessage ="if 8 or more one Upper ,lowwer ,number and nonAlphpitic")]
        public string Password { get; set; } = null!;
    }
}