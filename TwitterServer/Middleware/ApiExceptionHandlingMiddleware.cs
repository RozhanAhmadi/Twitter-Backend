using CommonObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TwitterServer.Exceptions;

namespace WebApi.Middlewares
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiExceptionHandlingMiddleware(
            RequestDelegate next
        )
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string result;

            if (ex is TwitterApiException edalatException)
            {
                result = JsonConvert.SerializeObject(new { Message = edalatException.Message, Code = 400 });
                context.Response.StatusCode = edalatException.Code;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = JsonConvert.SerializeObject(new { errors = "Internal Server Error!" });
            }

            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }
    }
}
