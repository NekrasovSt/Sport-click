using System.ComponentModel.DataAnnotations;

namespace СпортКлик.Models
{
    public class LogOnViewModel
    {
        [Display(Name="Имя пользователя")]
        [Required(ErrorMessage="Имя пользователя обязательно")]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязательно")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class RegisterModel : LogOnViewModel
    {
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        ErrorMessage = "Не допустимое значение электронной почты")]

        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePassword
    {
        [Required(ErrorMessage = "Старый пароль обязательно")]
        [Display(Name = "Старый пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Новый пароль обязательно")]
        [Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Новый пароль повторно обязательно")]
        [Display(Name = "Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string NewPasswordAway { get; set; }
    }
}