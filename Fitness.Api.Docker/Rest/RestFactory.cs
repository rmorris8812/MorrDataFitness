using Fitness.Api.Dtos;
using Fitness.Database.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Fitness.Api.Docker.Rest
{
    public static class RestFactory
    {
        public static RestResponse CreateResponse(HttpStatusCode statusCode, UserDto user, HttpRequest request)
        {
            var userResource = new UserResource()
            {
                Data = user
            };
            return new RestResponse()
            {
                Allowed = new List<string>() { "GET", "POST", "UPDATE", "DELETE" },
                ContentType = "application-json",
                ExtendedCode = 0,
                StatusCode = 200,
                HRef = request.GetDisplayUrl(),
                Resource = userResource
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
