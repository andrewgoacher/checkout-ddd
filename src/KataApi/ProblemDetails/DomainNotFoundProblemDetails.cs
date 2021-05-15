using Kata.Domain.Core;

namespace KataApi.ProblemDetails
{
    public class DomainNotFoundProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public DomainNotFoundProblemDetails(string uri, DomainException ex)
        {
            Status = 404;
            Type = "https://httpstatuses.com/404";
            this.Title = "Not Found";
            this.Instance = uri;
            this.Detail = ex.Description;
        }
    }
}
