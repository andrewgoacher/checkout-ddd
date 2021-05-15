using System;
namespace KataApi.ProblemDetails
{
    public class InternalServerErrorProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
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

