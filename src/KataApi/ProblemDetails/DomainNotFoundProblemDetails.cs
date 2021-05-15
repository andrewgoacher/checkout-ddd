using Kata.Domain.Core;

namespace KataApi.ProblemDetails
{
    /// <summary>
    /// Standardised Error message from when an entity or an aggregate root is not found.
    /// </summary>
    public class DomainNotFoundProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        /// <summary>
        /// Creates an instance of DomainNotFoundProblemDetails
        /// </summary>
        /// <param name="uri">The path to the resource that was requested</param>
        /// <param name="ex">The domain exception triggering the not found response</param>
        public DomainNotFoundProblemDetails(string uri, AggregateNotFoundException ex) : this(uri, ex as DomainException)
        {
        }

        /// <summary>
        /// Creates an instance of DomainNotFoundProblemDetails
        /// </summary>
        /// <param name="uri">The path to the resource that was requested</param>
        /// <param name="ex">The domain exception triggering the not found response</param>
        public DomainNotFoundProblemDetails(string uri, EntityNotFoundException ex) : this(uri, ex as DomainException)
        {
        }

        private DomainNotFoundProblemDetails(string uri, DomainException ex)
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
