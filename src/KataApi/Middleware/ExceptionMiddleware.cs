using System;
using System.Threading.Tasks;
using Kata.Domain.Core;
using KataApi.ProblemDetails;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KataApi.Middleware
{
    public class ExceptionMiddleware
    {
        public ExceptionMiddleware()
        {
        }

        public static async Task Run(HttpContext ctx)
        {
            var exceptionHandlerPathFeature =
                   ctx.Features.Get<IExceptionHandlerPathFeature>();

            var exception = exceptionHandlerPathFeature?.Error;

            if (exception == null) { return; }

            var loggerFactory = (ILoggerFactory)ctx.RequestServices.GetService(typeof(ILoggerFactory));
            if (loggerFactory != null)
            {
                var logger = loggerFactory.CreateLogger<ExceptionMiddleware>();

                logger.LogError(exception, "Unhandled exception caught");
            }

            var problemDetails = exception switch
            {
                AggregateNotFoundException e => NotFoundError(Path(ctx), e),
                EntityNotFoundException e => NotFoundError(Path(ctx), e),
                ValidationException e => ValidationError(Path(ctx), e),
                DomainException e => DomainError(Path(ctx), e),
                Exception e => InternalServerError(Path(ctx), e)
            };

            ctx.Response.StatusCode = problemDetails.Status.Value;
            ctx.Response.ContentType = "application/problem+json";
            await ctx.Response.WriteAsJsonAsync(problemDetails);
        }

        private static string Path(HttpContext ctx) => ctx.Request.Path.ToUriComponent();
        private static Microsoft.AspNetCore.Mvc.ProblemDetails NotFoundError(string path, AggregateNotFoundException ex) => new DomainNotFoundProblemDetails(path, ex);
        private static Microsoft.AspNetCore.Mvc.ProblemDetails NotFoundError(string path, EntityNotFoundException ex) => new DomainNotFoundProblemDetails(path, ex);
        private static Microsoft.AspNetCore.Mvc.ProblemDetails ValidationError(string path, ValidationException ex) => new DomainValidationProblemDetails(path, ex);
        private static Microsoft.AspNetCore.Mvc.ProblemDetails DomainError(string path, DomainException ex) => new DomainProblemDetails(path, ex);
        private static Microsoft.AspNetCore.Mvc.ProblemDetails InternalServerError(string path, Exception ex) => new InternalServerErrorProblemDetails(path, ex);
    }
}
