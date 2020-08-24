using Fitness.Api.Dtos;
using Fitness.Database.Api;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

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
                        var token = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString()));
                        user.Token = token;
                        databaseApi.UpdateUser(user);

                        var dto = new UserDto()
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Token = user.Token
                        };
                        return JsonConvert.SerializeObject(dto);
                    }
                }
            }
            HttpContext.Response.StatusCode = 401;
            return null;
        }
    }
}
