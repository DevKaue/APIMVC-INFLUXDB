using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.Repositories
{
    public interface IUserRepository
    {
        List<UserModel> GetAll();
        UserModel GetUsersById(int id);
        UserModel AddUser(UserModel user);
        UserModel EditUser(UserModel user);
        bool DeleteUser(int id);
    }
}