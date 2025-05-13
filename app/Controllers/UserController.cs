using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using app.Repositories;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    // [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            List<UserModel> users = _userRepository.GetAll();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult EditUser(int id)
        {
            UserModel user = _userRepository.GetUsersById(id);
            return View(user);
        }

        public IActionResult DeleteConfirmation(int id)
        {
            UserModel user = _userRepository.GetUsersById(id);
            return View(user);
        }



        [HttpPost("User/CreateUser")]
        public IActionResult CreateUser(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user = _userRepository.AddUser(user);

                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception error)
            {

                TempData["MensagemSucesso"] = $"Ops, nao foi possivel realizar o cadastro, tente novamente, detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _userRepository.DeleteUser(id);

                if (deleted)
                {
                    TempData["MensagemSucesso"] = "Usuario apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops!, não conseguimos apagar seu usuario";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops!, não foi possível apagar seu usuario, tente novamente. Erro: {ex.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpPost("User/Edit")]
        public IActionResult EditUser(UserNoPassword userNoPassword)
        {

            try
            {
                UserModel? user = null;

                if (ModelState.IsValid)
                {
                    user = new UserModel()
                    {
                        Id = userNoPassword.Id,
                        Name = userNoPassword.Name,
                        Login = userNoPassword.Login,
                        Email = userNoPassword.Email,
                        Profile = userNoPassword.Profile
                    };

                    user = _userRepository.EditUser(user);
                    TempData["MensagemSucesso"] = "Usuario atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Ops!, não foi possível atualizar seu usuario, tente novamente. Erro: {ex.Message}";
                return RedirectToAction("Index");
            }

            // Se houver algum erro no modelo ou uma exceção ao adicionar o contato, 
            // retorne a mesma view com os dados do contato para que o usuário possa corrigir
            return View("Index");

        }

    }
}