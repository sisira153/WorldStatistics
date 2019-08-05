using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WorldStatistics.ErrorHandling
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
                    await _next(context);
                }
                catch (Exception ex)
                {
                    await HandleException(context, ex);
                }
            }

            private Task HandleException(HttpContext context, Exception exception)
            {
                var code = HttpStatusCode.InternalServerError; // 500 if unexpected


                var result = JsonConvert.SerializeObject(new { error = exception.Message });

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;

                return context.Response.WriteAsync(result);
            }
    }
}
