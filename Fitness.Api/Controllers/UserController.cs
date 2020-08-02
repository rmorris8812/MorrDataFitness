using Fitness.Api.Dtos;
using Fitness.Database.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace Fitness.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public string Login(UserDto userDto)
        {
            using (var databaseApi = new DatabaseApi())
            {
                var user = databaseApi.GetUserByEmail(userDto.Email);
                if (user != null)
                {
                    var encodedPassword = Convert.ToBase64String(Encoding.ASCII.GetBytes(userDto.Password));
                    if (user.Password.Equals(encodedPassword))
                    {
                        return "84hfuihreiuwht85tgw4gjijrkg==";
                    }
                }
            }
            HttpContext.Response.StatusCode = 401;
            return null;
        }
    }
}
