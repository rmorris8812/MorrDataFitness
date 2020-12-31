// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System;
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
    [Route("api/dailygoal")]
    [ApiController]
    public class DailyGoalController : ControllerBase
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public RestResponse Get(long userId, int startIndex, int maxResults)
        {
            try
            {
                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var goal = databaseApi.GetDailyGoal(userId);
                    if (goal == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "Daily goal not found", Request);

                    return RestFactory.CreateResponse(HttpStatusCode.OK, Mappers.Map(goal), Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Get Daily Goal.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
    }
}
