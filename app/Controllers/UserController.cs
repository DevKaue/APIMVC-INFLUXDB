using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    // [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly InfluxDBService _influxDBService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, InfluxDBService influxDBService)
        {
            _logger = logger;
            _influxDBService = influxDBService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _influxDBService.QueryUsersAsync();
            return View(users);
        }

        // GetById

        // public async Task<IActionResult> GetUsersById(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var users = await _influxDBService.QueryUsersAsync();
        //     var user = users.FirstOrDefault(u => u.Id == id);

        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(user);
        // }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email")] UserModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var users = await _influxDBService.QueryUsersAsync();
                    user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
                    await _influxDBService.WriteUserAsync(user);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucess!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Ops!, nao foi possivel cadastrar o usuario. Erro: {ex.Message}";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("Cadastro", user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _influxDBService.QueryUsersAsync();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Metodos a Criar

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] UserModel user)
        // {
        //     if (id != user.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             await _influxDBService.UpdateUserAsync(user);
        //         }
        //         catch
        //         {
        //             return NotFound();
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(user);
        // }

        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var users = await _influxDBService.QueryUsersAsync();
        //     var user = users.FirstOrDefault(u => u.Id == id);

        //     if (user == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(user);
        // }

        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var user = await _influxDBService.GetUserByIdAsync(id);
        //     await _influxDBService.DeleteUserAsync(user);
        //     return RedirectToAction(nameof(Index));
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}