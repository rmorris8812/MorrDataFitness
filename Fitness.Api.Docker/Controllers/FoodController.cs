// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System;
using System.Collections.Generic;
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
                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var foods = databaseApi.GetFood(startIndex, maxResults);
                    var dtos = new List<FoodDto>();
                    foreach (var food in foods)
                    {
                        dtos.Add(Mappers.Map(food));
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
                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var foods = databaseApi.SearchFood(criteria);
                    var dtos = new List<FoodDto>();
                    foreach (var food in foods)
                    {
                        dtos.Add(Mappers.Map(food));
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
        [HttpPost]
        public async Task<RestResponse> Create(FoodDto foodDto)
        {
            try
            {
                if (foodDto == null)
                    throw new ArgumentNullException("A food item is required.");

                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var food = Mappers.Map(foodDto);

                    var foodId = await databaseApi.InsertFoodAsync(food, CancellationToken.None);
                    if (foodId == 0)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "Unable to insert new food item!", Request);
                    food.FoodId = foodId;
                    return RestFactory.CreateResponse(HttpStatusCode.OK, foodDto, Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Create Food.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
        [HttpPut]
        public async Task<RestResponse> Update(FoodDto foodDto)
        {
            try
            {
                if (foodDto == null)
                    throw new ArgumentNullException("Food item is required.");

                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var food = await databaseApi.GetFoodByIdAsync(foodDto.FoodId, CancellationToken.None);
                    if (food == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "User not found!", Request);

                    food = Mappers.Map(foodDto);

                    await databaseApi.UpdateFoodAsync(food, CancellationToken.None);
                    return RestFactory.CreateResponse(HttpStatusCode.OK, true, Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Update Food.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
        [HttpDelete]
        public async Task<RestResponse> Delete(long foodId)
        {
            try
            {
                if (foodId == 0)
                    throw new ArgumentNullException("User id is required.");

                using (var databaseApi = ServiceFactory.GetService<IFitnessService>())
                {
                    var food = await databaseApi.GetFoodByIdAsync(foodId, CancellationToken.None);
                    if (food == null)
                        return RestFactory.CreateErrorResponse(HttpStatusCode.NotFound, "Food item not found!", Request);

                    await databaseApi.DeleteUserAsync(foodId, CancellationToken.None);
                    return RestFactory.CreateResponse(HttpStatusCode.OK, true, Request);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error in Delete Food.");
                return RestFactory.CreateErrorResponse(HttpStatusCode.InternalServerError, e, Request);
            }
        }
    }
}
