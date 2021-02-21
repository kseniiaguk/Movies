using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models.ViewModels
{
    [Keyless]
    [NotMapped]
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Пароль ещё разочек")]
        public string PasswordConfirmation { get; set; }

        public UserModel CreateUserModel() => new UserModel
                                              {
                                                  Name = Name, // слева свойства result, а справа - свойства текущего объекта (экземпляра)
                                                  Email = Email,
                                                  Password = Password
                                              };
    }
}
