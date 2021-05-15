using Kata.Domain.Core;

namespace KataApi.ProblemDetails
{
    public class DomainNotFoundProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public DomainNotFoundProblemDetails(string uri, DomainException ex)
        {
            Status = 404;
            // Would be handy to have our own url docs for this where we could explain what the missing type is etc
            Type = "https://httpstatuses.com/404";
            this.Title = "Not Found";
            this.Instance = uri;
            this.Detail = ex.Description;
        }
    }
}
