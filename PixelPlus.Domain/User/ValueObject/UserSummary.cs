using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelPlus.Domain.User.ValueObject
{
    public class UserSummary
    {
        public UserSummary(string userName, string token, DateTime expirationDate)
        {
            UserName = userName;
            Token = token;
            ExpirationDate = expirationDate;
        }

        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
