using Kata.Domain.Core;

namespace KataApi.ProblemDetails
{
    /// <summary>
    /// A standardised error message for a <see cref="ValidationException"/>.
    /// </summary>
    public class DomainValidationProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        /// <summary>
        /// Creates an instance of DomainValidationProblemDetails
        /// </summary>
        /// <param name="uri">The path to the resource that caused the validation error</param>
        /// <param name="ex">The <see cref="ValidationException"/></param>
        public DomainValidationProblemDetails(string uri, ValidationException ex)
        {
            Status = 400;
            // A better url here would be one that gives an explanation of each validation error we have
            Type = "https://httpstatuses.com/400";
            this.Title = "Validation Error";
            this.Instance = uri;
            this.Detail = ex.Description;
        }
    }
}
