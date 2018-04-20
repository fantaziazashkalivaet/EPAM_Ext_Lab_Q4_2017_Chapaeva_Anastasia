namespace FileStorage.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegistrationViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "Логин должен содержать от 4-х до 10-и символов", MinimumLength = 4)]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Пароль должен содержать от 4-х до 20-и символов", MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}