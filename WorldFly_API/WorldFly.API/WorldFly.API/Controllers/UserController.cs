using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.RegularExpressions;
using WorldFly.API.Data;
using WorldFly.API.Helpers;
using WorldFly.API.Models;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace WorldFly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WorldFlyDbContext _authContext;
        public UserController(WorldFlyDbContext _authContextt)
        {
           this._authContext = _authContextt;
            
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if(userObj == null)
            {
                return BadRequest();
            }

            var user = await _authContext.User
                .FirstOrDefaultAsync(x=>x.UserEmail == userObj.UserEmail) ;
            if (user == null)
                return NotFound(new { Message = "User not found!" });

            if(!PasswordHasher.VerifyPassword(userObj.UserPassowrd,user.UserPassowrd))
            {
                return BadRequest(new { Message = "Password is incorrect!" });
            }

            user.Token = CreateJwt(user);

            return Ok(new
            {
                Token = user.Token,
                message = "Login Success!"
            }); ;

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null) return BadRequest();

            //Check Email
            if (await CheckEmailExistAsync(userObj.UserEmail))
                return BadRequest(new {Message = "Email Already Exists!"});



            //Check password strength
            var pass = CheckPasswordStrength(userObj.UserPassowrd);
            if(!string.IsNullOrEmpty(pass))
                return BadRequest(new {Message = pass.ToString() });


            userObj.UserPassowrd = PasswordHasher.HashPassword(userObj.UserPassowrd);
            
            await _authContext.User.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                message = "Register Success!"
            });
        }

        private Task<bool> CheckEmailExistAsync(string email)
          => _authContext.User.AnyAsync(x => x.UserEmail == email);

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if(password.Length < 8)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]")
                && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]")) 
                sb.Append("Password shuold contain special chars" + Environment.NewLine);

            return sb.ToString();

        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers()
        {
            return Ok(await _authContext.User.ToListAsync());

        }
    }
}
