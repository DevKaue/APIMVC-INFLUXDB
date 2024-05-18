using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using app.Helpers;
using app.Models;
using app.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessions _sessions;

        public LoginController(IUserRepository userRepository, ISessions sessions)
        {
            _userRepository = userRepository;
            _sessions = sessions;
        }

        public IActionResult Index()
        {
            //Se o Usuario estiver Logado, redirecionar para Home
            if (_sessions.FindSessionForUser() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Exit()
        {
            _sessions.RemoveSessionForUser();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _userRepository.FindForLogin(loginModel.Login);
                    if (user != null)
                    {
                        if (user.PasswordValid(loginModel.Password))
                        {
                            _sessions.CreateSessionForUser(user);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha do usuario e' invalida. Por favor, tente novamente";
                    }
                    TempData["MensagemErro"] = $"Usuario e/ou senha invalidos(s). Por favor, tente novamente";
                }

                return View("Index");

            }
            catch (Exception ex)
            {

                TempData["MensagemErro"] = $"Ops!, não foi possível realizar o login, tente novamente. Erro: {ex.Message}";
                return RedirectToAction("Index");
            }

        }

    }
}