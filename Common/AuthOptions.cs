using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Common
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int LifeTime { get; set; }

        public SymmetricSecurityKey GetSymetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
        
    }
}
