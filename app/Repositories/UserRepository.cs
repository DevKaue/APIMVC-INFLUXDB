using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.Models;

namespace app.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BancoContext _bancoContext;

        public UserRepository(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public List<UserModel> GetAll()
        {
            return _bancoContext.Users.ToList();
        }

        public UserModel GetUsersById(int id)
        {
            return _bancoContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public UserModel FindForLogin(string login)
        {
            return _bancoContext.Users.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UserModel AddUser(UserModel user)
        {
            //inserindo dados no Banco
            user.DateRegister = DateTime.Now;
            _bancoContext.Users.Add(user);
            _bancoContext.SaveChanges();
            return user;
        }

        public UserModel EditUser(UserModel user)
        {
            UserModel userDB = GetUsersById(user.Id);

            if (userDB == null) throw new Exception("Houve um erro na atualizacao do contato!");

            userDB.Name = user.Name;
            userDB.Email = user.Email;
            userDB.Login = user.Login;
            _bancoContext.Users.Update(userDB);
            _bancoContext.SaveChanges();

            return userDB;

        }

        public bool DeleteUser(int id)
        {
            UserModel userDB = GetUsersById(id);

            if (userDB == null) throw new Exception("Houve um erro na atualizacao do contato!");

            _bancoContext.Users.Remove(userDB);
            _bancoContext.SaveChanges();

            return true;

        }


    }
}