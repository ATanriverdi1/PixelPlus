using Microsoft.IdentityModel.Tokens;
using PixelPlus.Domain.Extensions;
using PixelPlus.Domain.User.ValueObject;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixelPlus.Domain.User
{
    public class UserAggregate : BaseRoot
    {
        private static string PasswordKey => "n19t!45nds1gfk495pdk";

        public UserAggregate() { }

        public UserAggregate(string firstName, string lastName, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Password = GeneratePassword(password);
            SetAsCreated();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserSummary Authenticate(string secretKey)
        {
            var userSummary = GenarateJwtToken(secretKey);
            return userSummary;
        }

        private UserSummary GenarateJwtToken(string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userSummary = new UserSummary(UserName, tokenHandler.WriteToken(token), tokenDescriptor.Expires.Value);
            return userSummary;
        }

        public string GeneratePassword(string password)
        {
            return password.ToHMAC(PasswordKey);
        }

    }
}
