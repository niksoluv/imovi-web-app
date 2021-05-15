using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace imovi.ViewModels {
    public class RegisterModel {
        [Required(ErrorMessage = "missing e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "missing password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "wrong password")]
        public string ConfirmPassword { get; set; }
    }
}
