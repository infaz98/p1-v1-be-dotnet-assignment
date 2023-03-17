using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace API.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var result = JsonConvert.SerializeObject(new { Message = ex.Message, Source = ex.Source, Type = ex.GetType().Name });
                Log.Error(result);
                await context.Response.WriteAsync(result);
            }
        }
    }
}
