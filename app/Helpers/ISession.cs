using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models;

namespace app.Helpers
{
    public interface ISessions
    {
        void CreateSessionForUser(UserModel user);
        void RemoveSessionForUser();
        UserModel FindSessionForUser();
    }
}