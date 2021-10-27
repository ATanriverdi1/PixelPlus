using PixelPlus.Application.User.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelPlus.Api.Models.Account.Requests
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public AuthenticateCommand ToCommand(string jwtKey)
        {
            return new(UserName, Password, jwtKey);
        }
    }
}
