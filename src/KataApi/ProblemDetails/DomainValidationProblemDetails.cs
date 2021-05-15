using Kata.Domain.Core;

namespace KataApi.ProblemDetails
{
    public class DomainValidationProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public DomainValidationProblemDetails(string uri, ValidationException ex)
        {
            Status = 400;
            Type = "https://httpstatuses.com/400";
            this.Title = "Validation Error";
            this.Instance = uri;
            this.Detail = ex.Description;
        }
    }
}
