using Fitness.Api.Docker.Rest;
using Fitness.Api.Dtos;
using Fitness.Database.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Fitness.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    //Authorize]
    public class UserController : ControllerBase
    {
        public async Task<RestResponse> Get(string email, string tenantId)
        {
            try
            {
                if (email == null)
                    throw new ArgumentNullException("email is required.");
                if (email == null)
                    throw new ArgumentNullException("tenantId is required.");

                using (var databaseApi = new DatabaseApi())
                {
                    var user = await databaseApi.GetUserByEmailAsync(email, tenantId, CancellationToken.None);
                    if (user == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "User not found!", Request);
                    
                    var dto = new UserDto()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Token = user.Token,
                        TenantId = user.TenantId
                    };
                    return RestFactory.CreateResponse(HttpStatusCode.OK, dto, Request);
                }
            } 
            catch (Exception e)
            {
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
    }
}
