namespace FileStorage.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login:")]
        [StringLength(10, ErrorMessage = "Логин должен содержать от 4-х до 10-и символов", MinimumLength = 4)]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Password:")]
        [StringLength(20, ErrorMessage = "Пароль должен содержать от 4-х до 20-и символов", MinimumLength = 4)]
        public string Password { get; set; }
    }
}