using Common;
using Library.Auth.Api.Domain;
using Library.Auth.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Library.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthContext dbContext;
        private readonly IOptions<AuthOptions> authOptions;
        //не стал делать отдельную прослойку для работы с контекстом
        //инжектировал его напрямую
        public AuthController(AuthContext dbContext, IOptions<AuthOptions> options)
        {
            this.dbContext = dbContext;
            this.authOptions = options;
        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginForm loginModel)
        {
            //Тут хорошо бы добавить обработку исключений...
            var user = Authenticate(loginModel.UserName, loginModel.Password);
            if (user != null)
            {
                var jwtToken = GenerateJwt(user);

                return Ok(new
                {
                    Token = jwtToken
                });
            }
            else
            {
                return Unauthorized();
            }
        }
        //Проверяем логин и пароль юзера
        private Account Authenticate(string login, string password)
        {
            var hashPassword = new PasswordHasher<Account>().HashPassword(null, password);
            //Тут нужно захешировать пароль, сделаю завтра
            return dbContext.Accounts.SingleOrDefault(x => x.UserName == login && x.PasswordHash == hashPassword);
        }

        //Генерируем токен
        private string GenerateJwt(Account user)
        {
            var authParams = this.authOptions.Value;
            var securityKey = authParams.GetSymetricSecurityKey();

            //Подпись для токена
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role", dbContext.Roles.First(r=>r.Id == user.RoleId).Name)
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims, expires: DateTime.Now.AddSeconds(authParams.LifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
    
}
