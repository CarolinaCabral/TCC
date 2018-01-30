using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeeExperience.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}