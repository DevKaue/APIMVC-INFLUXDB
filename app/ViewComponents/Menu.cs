using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using InfluxDB.Client.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace app.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessionUser = HttpContext.Session.GetString("sessionUserLogged");

            if (string.IsNullOrEmpty(sessionUser)) return null;

            UserModel user = JsonConvert.DeserializeObject<UserModel>(sessionUser);

            return View(user);
        }
    }
}