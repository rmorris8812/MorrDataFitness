// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Net;
using Fitness.Api.Configuration;
using Fitness.Api.Dtos;
using Fitness.Api.Rest;
using Fitness.Database.Api;
using Fitness.Database.Api.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;


namespace Fitness.Api.Controllers
{
    [Route("api/userfood")]
    [ApiController]
    public class UserFoodController : ControllerBase
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public RestResponse Get(long userId, int startIndex, int maxResults)
        {
            try
            {
                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var foods = databaseApi.GetUserFood(userId, startIndex, maxResults);
                    var dtos = new List<UserFoodDto>();
                    foreach (var food in foods)
                    {
                        dtos.Add(Mappers.Map(food));
                    }
                    return RestFactory.CreateResponse(HttpStatusCode.OK, dtos, Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Get Goal.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
    }
}
