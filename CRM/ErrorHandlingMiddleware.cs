using CRM.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRM
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ApiException ae)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.BadRequest;

                ApiError error = new()
                {
                    Error = new ErrorMessage()
                    {
                        Message = ae.Message
                    }
                };

                var errorJson = JsonSerializer.Serialize(error);

                await response.WriteAsync(errorJson);
            }
            catch (Exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ApiError error = new()
                {
                    Error = new ErrorMessage()
                    {
                        Message = "Error interno del servidor"
                    }
                };

                var errorJson = JsonSerializer.Serialize(error);

                await response.WriteAsync(errorJson);
            }
        }
    }

    public class ApiError
    {
        public ErrorMessage Error { get; set; }
    }

    public class ErrorMessage
    {
        public string Message { get; set; }
    }
}
