using System.ComponentModel.DataAnnotations;

namespace PetsBook.API.ViewModels.User
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
