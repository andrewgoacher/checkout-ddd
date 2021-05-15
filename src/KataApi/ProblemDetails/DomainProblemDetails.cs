using Kata.Domain.Core;

namespace KataApi.ProblemDetails
{
    public class DomainProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public DomainProblemDetails(string uri, DomainException ex)
        {
            Status = 400;
            Type = "https://httpstatuses.com/400";
            this.Title = "Domain Error";
            this.Instance = uri;
            this.Detail = ex.Description;
        }
    }
}
