using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileStorage.DAL.Models
{
    public class User
    {
        public int UserID { get; set; }

        public Role Role { get; set; }

        [Required]
        [Display(Name = "Login:")]
        [StringLength(10, ErrorMessage = "Логин должен содержать от 4-х до 10-и символов", MinimumLength = 4)]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Password:")]
        [StringLength(20, ErrorMessage = "Пароль должен содержать от 4-х до 20-и символов", MinimumLength = 4)]
        public string PasswordHash { get; set; }
    }
}