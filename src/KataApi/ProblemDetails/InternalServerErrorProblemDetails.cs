using System;
namespace KataApi.ProblemDetails
{
    /// <summary>
    /// Represents a standard error message for any exception that we don't explicitely handle
    /// </summary>
    public class InternalServerErrorProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        /// <summary>
        /// Creates an instance of InternalServerErrorProblemDetails
        /// </summary>
        /// <param name="uri">The path to the resource that throws the error</param>
        /// <param name="ex">The <see cref="Exception"/> that caused the error</param>
        public InternalServerErrorProblemDetails(string uri, Exception ex)
        {
            Status = 500;
            Type = "https://httpstatuses.com/500";
            this.Title = "Internal Server Error";
            this.Instance = uri;
            this.Detail = ex.Message;
        }
    }
}

