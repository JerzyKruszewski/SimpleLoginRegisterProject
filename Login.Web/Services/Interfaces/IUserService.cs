using Login.Web.Data.Model;
using Login.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(string login);

        Task Register(UserViewModel userViewModel);

        public bool ComparePasswords(string hashed, string password);
    }
}
