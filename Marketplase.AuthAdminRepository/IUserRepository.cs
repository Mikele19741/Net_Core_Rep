using LAOL.Marketplase.AuthAdminRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAOL.Marketplase.AuthAdminRepository
{
    public interface IUserRepository
    {
        User GetUser(string userName, string password);
        bool CreateUser(UserDto user);
    }
}
