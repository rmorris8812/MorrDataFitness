using Fitness.Api.Dtos;
using Fitness.Database.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Fitness.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<UserDto> Get(string email, string tenantId)
        {
            using (var databaseApi = new DatabaseApi())
            {
                var user = await databaseApi.GetUserByEmailAsync(email, tenantId, CancellationToken.None);
                var dto = new UserDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Token = user.Token
                };
                return dto;
            }
        }
    }
}
