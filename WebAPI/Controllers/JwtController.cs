using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebAPI.BL.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] AUser login)
        {
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        private string GenerateJSONWebToken(AUser userInfo)
        {
            var securityKey = 
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _config["Jwt:Key"]));
                
            var credentials = 
                new SigningCredentials(
                        securityKey, 
                        SecurityAlgorithms.HmacSha256);

            var token = 
                new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    null,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        private AUser AuthenticateUser(AUser login)
        {
            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information    
            if (login.Loginname == "Davor")
            {
                return new AUser { 
                    Firstname = "Davor", 
                    Lastname = "Davorović", 
                    Email = "test.btest@gmail.com" };

            }

            return null;
        }
    }
}
