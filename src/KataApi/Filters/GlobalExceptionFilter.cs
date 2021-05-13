using Kata.Domain.Core;
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
                switch (context.Exception)
                {
                    case AggregateNotFoundException anf:
                        {
                            context.Result = new NotFoundObjectResult(anf.Description);
                            context.ExceptionHandled = true;
                            break;
                        }
                    case EntityNotFoundException enf:
                        {
                            context.Result = new NotFoundObjectResult(enf.Description);
                            context.ExceptionHandled = true;
                            break;
                        }
                    case ValidationException ve:
                        {
                            context.Result = new BadRequestObjectResult(ve.Description);
                            context.ExceptionHandled = true;
                            break;
                        }
                    case DomainException de:
                        {
                            context.Result = new BadRequestObjectResult(de.Description);
                            context.ExceptionHandled = true;
                            break;
                        }
                }
            }
        }
    }
}
