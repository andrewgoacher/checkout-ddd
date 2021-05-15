using System;
using Kata.Domain.Core;
using KataApi.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KataApi.Filters
{
    public class GlobalExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                var problemDetails = context.Exception switch
                {
                    AggregateNotFoundException e => NotFoundError(Path(context), e),
                    EntityNotFoundException e => NotFoundError(Path(context), e),
                    ValidationException e => ValidationError(Path(context), e),
                    DomainException e => DomainError(Path(context), e),
                    Exception e => InternalServerError(Path(context), e)
                };

                context.Result = new ObjectResult(problemDetails)
                {
                    ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection
                    {
                        "application/json+problem"
                    }
                };
                context.ExceptionHandled = true;
            }

        }

        private static string Path(ActionExecutedContext ctx) => ctx.HttpContext.Request.Path.ToUriComponent();
        private static Microsoft.AspNetCore.Mvc.ProblemDetails NotFoundError(string path, DomainException ex) => new DomainNotFoundProblemDetails(path, ex);
        private static Microsoft.AspNetCore.Mvc.ProblemDetails ValidationError(string path, ValidationException ex) => new DomainValidationProblemDetails(path, ex);
        private static Microsoft.AspNetCore.Mvc.ProblemDetails DomainError(string path, DomainException ex) => new DomainProblemDetails(path, ex);
        private static Microsoft.AspNetCore.Mvc.ProblemDetails InternalServerError(string path, Exception ex) => new InternalServerErrorProblemDetails(path, ex);
    }
}
