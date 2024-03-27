using Contracts;
using Entites.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace CompanyEmployees.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            //we create an extension method, on top of the WebApplication type, and we call the UseExceptionHandler method.
            //That method adds a middleware to the pipeline that will catch exceptions, log them, and re - execute the request in an alternate pipeline.

            app.UseExceptionHandler(appError =>       //we use the appError variable of the IApplicationBuilder type.

            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error.",
                        }.ToString());
                    }
                });
            });
        }

    }
}
