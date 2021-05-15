using Kata.Domain.Core;

namespace KataApi.ProblemDetails
{
    public class DomainProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public DomainProblemDetails(string uri, DomainException ex)
        {
            Status = 400;
            // We don't throw domain exceptions directly so this is where something has gone wrong in the domain
            // and we haven't handled it specifically with another catch all.  This should probably be OK with a 400
            Type = "https://httpstatuses.com/400";
            this.Title = "Domain Error";
            this.Instance = uri;
            this.Detail = ex.Description;
        }
    }
}
