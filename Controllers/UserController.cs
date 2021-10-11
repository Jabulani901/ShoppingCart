using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingCart.Helpers;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserData _userData;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;

        public UserController(IConfiguration config, ITokenService tokenService, IUserData userData)
        {
            _config = config;
            _tokenService = tokenService;
            _userData = userData;
        }

        [HttpGet("GetUser")]
        public async Task<IEnumerable<User>> GetUser()
        {
            return await this._userData.GetUsers();
        }

        [HttpPost("LoginUser")]
        public async Task<LoginRequest> LogIn([FromBody] LoginRequest loginRequest)
        {
           
            var validUser = await this._userData.LogInUser(loginRequest.UserName, loginRequest.Password);

            if (validUser != null)
            {
                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), loginRequest);
                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("AccessToken", generatedToken);                    
                }
                else
                {
                    HttpContext.Session.SetString("AccessToken", "");
                }
            }
            return validUser;
        }

        [HttpPost("CreateUser")]
        public async Task AddNewUser([FromBody] User user)
        {
            await this._userData.AddUser(user);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task UpdateUser(int UserId, [FromBody] User user)
        {
            await this._userData.UpdateUser(user);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task DeleteUser(int UserId)
        {
            await this._userData.DeleteUser(UserId);
        }
    }
}
