using LAOL.Marketplase.AuthAdminRepository;
using LAOL.Marketplase.AuthAdminRepository.Models;
using LAOL.Marketplase.WEB.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace LAOL.Marketplase.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRep;
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration, IUserRepository userRep)
        {
            _configuration = configuration;
            _userRep=userRep;
        }
        [HttpPost("register")]
        public bool Register(UserDto request)
        {
        
            var user=_userRep.CreateUser(request);
           
           

            return user;
        }

      

        [HttpPost("login")]
        public async Task<User>Login(UserDto request)
        {
            
           var  user=_userRep.GetUser(request.Email, request.Password);
              var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Email.ToString()),
     
        };


            //var hmac = new HMACSHA512();
            //   var  passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password));
            //user.PasswordHash = passwordHash;

           
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
          

            var tokenDescriptor1 = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signinCredentials
         
            };

            var tokenObject1 = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor1);
            string Token = new JwtSecurityTokenHandler().WriteToken(tokenObject1);
            user.JwtToken = Token;
            
            return user;
          

           
        }

       
      
       
       
    }
}
