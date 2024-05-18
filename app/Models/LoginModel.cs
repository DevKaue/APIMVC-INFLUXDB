using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o Login")]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage = "Digite a Password")]
        public string Password { get; set; } = string.Empty;
    }
}