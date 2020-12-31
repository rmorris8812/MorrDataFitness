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
    [Route("api/goal")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public RestResponse Get(long userId, int startIndex, int maxResults)
        {
            try
            {
                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var goals = databaseApi.GetUserGoals(userId, startIndex, maxResults);
                    var dtos = new List<GoalDto>();
                    foreach (var goal in goals)
                    {
                        dtos.Add(Mappers.Map(goal));
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
