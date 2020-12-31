// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Fitness.Api.Configuration;
using Fitness.Api.Dtos;
using Fitness.Api.Rest;
using Fitness.Database.Api;
using Fitness.Database.Api.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Fitness.Api.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public async Task<RestResponse> Get(long userId)
        {
            try
            {
                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var measurement = databaseApi.GetMeasurement(userId);
                    if (measurement == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "Measurement not found", Request);

                    var goal = databaseApi.GetUserGoal(userId);
                    if (goal == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "User goal not found", Request);

                    var dailyGoal = databaseApi.GetDailyGoal(userId);
                    if (dailyGoal == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "Daily goal not found", Request);

                    var userFoods = databaseApi.GetUserFood(userId, 0, 1000, DateTime.Now);

                    var dashboard = new DashboardDto()
                    {
                        UserId = userId,
                        DailyGoal = Mappers.Map(dailyGoal),
                        Goal = Mappers.Map(goal),
                        LastMeasurment = Mappers.Map(measurement)
                    };
                    foreach (var userFood in userFoods)
                    {
                        var food = await databaseApi.GetFoodByIdAsync(userFood.FoodId, CancellationToken.None);
                        if (food == null)
                            continue;
                        dashboard.CaloriesConsumed += food.Calories;
                        dashboard.Carbs += food.Carbs;
                        dashboard.Fat += food.Fat;
                        dashboard.Salt += food.Salt;
                        dashboard.Sugar += food.Sugar;
                        dashboard.Protein += food.Protein;
                    }
                    return RestFactory.CreateResponse(HttpStatusCode.OK, dashboard, Request);
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
