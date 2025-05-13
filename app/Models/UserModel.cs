using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using app.Enums;

namespace app.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do Usuario")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o login do Usuario")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o e-mail do Usuario")]
        [EmailAddress(ErrorMessage = "O e-mail informado e' invalido!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite a senha do Usuario")]
        public string Password { get; set; } = string.Empty;
        public DateTime DateRegister { get; set; }

        [Required(ErrorMessage = "Digite o perfil do Usuario")]
        public ProfilesEnum? Profile { get; set; }


        public bool PasswordValid(string password)
        {
            return Password == password;
        }

    }
}