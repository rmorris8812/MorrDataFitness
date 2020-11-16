// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System;
using System.Collections.Generic;
using System.Net;
using Fitness.Api.Dtos;
using Fitness.Api.Rest;
using Fitness.Database.Api;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Fitness.Api.Docker.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public RestResponse Get(int startIndex, int maxResults)
        {
            try
            {
                using (var databaseApi = new DatabaseApi())
                {
                    var foods = databaseApi.GetFood(startIndex, maxResults);
                    var dtos = new List<FoodDto>();
                    foreach (var food in foods)
                    {
                        dtos.Add(new FoodDto() 
                        {
                            Name = food.Name,
                            Calories = food.Calories,
                            FoodId = food.FoodId
                        });
                    }
                    return RestFactory.CreateResponse(HttpStatusCode.OK, dtos, Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Get Food.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
        [HttpGet]
        public RestResponse Search(string criteria)
        {
            try
            {
                using (var databaseApi = new DatabaseApi())
                {
                    var foods = databaseApi.SearchFood(criteria);
                    var dtos = new List<FoodDto>();
                    foreach (var food in foods)
                    {
                        dtos.Add(new FoodDto()
                        {
                            Name = food.Name,
                            Calories = food.Calories,
                            FoodId = food.FoodId
                        });
                    }
                    return RestFactory.CreateResponse(HttpStatusCode.OK, dtos, Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Get Food.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
    }
}
