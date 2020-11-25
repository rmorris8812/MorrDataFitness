// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Fitness.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Net;

namespace Fitness.Api.Rest
{
    public static class RestFactory
    {
        public static RestResponse CreateResponse(HttpStatusCode statusCode, UserDto user, HttpRequest request)
        {
            var dataResource = new UserResource()
            {
                Data = user
            };
            return CreateResponse(statusCode, dataResource, request);
        }
        public static RestResponse CreateResponse(HttpStatusCode statusCode, List<FoodDto> food, HttpRequest request)
        {
            var dataResource = new FoodArrayResource()
            {
                Data = food
            };
            return CreateResponse(statusCode, dataResource, request);
        }
        public static RestResponse CreateResponse(HttpStatusCode statusCode, FoodDto food, HttpRequest request)
        {
            var dataResource = new FoodResource()
            {
                Data = food
            };
            return CreateResponse(statusCode, dataResource, request);
        }
        public static RestResponse CreateResponse(HttpStatusCode statusCode, string data, HttpRequest request)
        {
            var dataResource = new StringResource()
            {
                Data = data
            };
            return CreateResponse(statusCode, dataResource, request);
        }
        public static RestResponse CreateResponse(HttpStatusCode statusCode, long data, HttpRequest request)
        {
            var dataResource = new LongResource()
            {
                Data = data
            };
            return CreateResponse(statusCode, dataResource, request);
        }
        public static RestResponse CreateResponse(HttpStatusCode statusCode, bool data, HttpRequest request)
        {
            var dataResource = new BoolResource()
            {
                Data = data
            };
            return CreateResponse(statusCode, dataResource, request);
        }
        private static RestResponse CreateResponse(HttpStatusCode statusCode, IRestResource dataResource, HttpRequest request)
        {
            return new RestResponse()
            {
                Allowed = new List<string>() { "GET", "POST", "UPDATE", "DELETE" },
                ContentType = "application-json",
                ExtendedCode = 0,
                StatusCode = 200,
                HRef = request.GetDisplayUrl(),
                Resource = dataResource
            };
        }
        public static RestResponse CreateErrorResponse(HttpStatusCode statusCode, Exception ex, HttpRequest request)
        {
            return CreateErrorResponse(statusCode, ex.Message, request);
        }
        public static RestResponse CreateErrorResponse(HttpStatusCode statusCode, string message, HttpRequest request)
        {
            return new RestResponse()
            {
                Allowed = new List<string>() { "GET", "POST", "UPDATE", "DELETE" },
                ContentType = "application-json",
                ExtendedCode = 0,
                StatusCode = (int)statusCode,
                HRef = request.GetDisplayUrl(),
                Message = message
            };
        }
    }
}
