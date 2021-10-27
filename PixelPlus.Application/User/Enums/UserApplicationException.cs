using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Application.User.Enums
{
    public static class UserApplicationException
    {
        public static string UserNotFound => "USER_NOT_FOUND";
        public static string UserNameOrPasswordWrong => "USER_NAME_OR_PASSWORD_WRONG";
        public static string UserNameAlreadyExist => "USER_NAME_ALREADY_EXIST";
    }
}
