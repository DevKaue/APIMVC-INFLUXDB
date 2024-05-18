using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using Newtonsoft.Json;

namespace app.Helpers
{
    public class Session : ISessions
    {
        private readonly IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UserModel FindSessionForUser()
        {
            //Fazendo a busca da sessao do Usuario

            string sessionUser = _httpContext.HttpContext.Session.GetString("sessionUserLogged");
            if (string.IsNullOrEmpty(sessionUser)) return null;
            return JsonConvert.DeserializeObject<UserModel>(sessionUser);
        }

        public void CreateSessionForUser(UserModel user)
        {

            //Criando a sessao do Usuario

            //Metodologia quase do mesmo conceito de JWT, utiliando TOKEN

            //Passando de uma string para um obj JSON
            string value = JsonConvert.SerializeObject(user);

            _httpContext.HttpContext.Session.SetString("sessionUserLogged", value);
        }
        public void RemoveSessionForUser()
        {
            _httpContext.HttpContext.Session.Remove("sessionUserLogged");
        }

    }
}