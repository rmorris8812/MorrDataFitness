// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Fitness.Api.Configuration;
using Fitness.Api.Dtos;
using Fitness.Api.Rest;
using Fitness.Database.Api;
using Fitness.Database.Api.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Fitness.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    //Authorize]
    public class UserController : ControllerBase
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        
        [HttpGet]
        public async Task<RestResponse> Get(string email, string tenantId)
        {
            try
            {
                if (email == null)
                    throw new ArgumentNullException("email is required.");
                if (tenantId == null)
                    throw new ArgumentNullException("tenantId is required.");

                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var user = await databaseApi.GetUserByEmailAsync(email, tenantId, CancellationToken.None);
                    if (user == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "User not found!", Request);

                    var dto = Mappers.Map(user);
                    return RestFactory.CreateResponse(HttpStatusCode.OK, dto, Request);
                }
            } 
            catch (Exception e)
            {
                _logger.Error(e, "Error in Get User.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
        [HttpPost]
        public async Task<RestResponse> Create(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    throw new ArgumentNullException("User is required.");
                if (userDto.Email == null)
                    throw new ArgumentNullException("User email is required.");
                if (userDto.Email == null)
                    throw new ArgumentNullException("User password is required.");
                if (userDto.TenantId == null)
                    throw new ArgumentNullException("User tenant id is required.");

                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var user = Mappers.Map(userDto);
                    user.Email = userDto.Email; // Mappers.Mapper does not set this becuase it is an alternate key

                    var userId = await databaseApi.InsertUserAsync(user, CancellationToken.None);
                    if (userId == 0)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "User not found!", Request);
                    userDto.Id = userId;
                    return RestFactory.CreateResponse(HttpStatusCode.OK, userDto, Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Create User.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
        [HttpPut]
        public async Task<RestResponse> Update(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    throw new ArgumentNullException("User is required.");

                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var user = await databaseApi.GetUserByEmailAsync(userDto.Email, userDto.TenantId, CancellationToken.None);
                    if (user == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "User not found!", Request);

                    user = Mappers.Map(userDto);
                    
                    await databaseApi.UpdateUserAsync(user, CancellationToken.None);
                    return RestFactory.CreateResponse(HttpStatusCode.OK, true, Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Update User.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
        [HttpDelete]
        public async Task<RestResponse> Delete(long userId)
        {
            try
            {
                if (userId == 0)
                    throw new ArgumentNullException("User id is required.");

                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var user = await databaseApi.GetUserByIdAsync(userId, CancellationToken.None);
                    if (user == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "User not found!", Request);

                    await databaseApi.DeleteUserAsync(userId, CancellationToken.None);
                    return RestFactory.CreateResponse(HttpStatusCode.OK, true, Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Delete User.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
    }
}
