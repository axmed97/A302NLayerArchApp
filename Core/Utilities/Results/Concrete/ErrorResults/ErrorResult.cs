using System.Net;

namespace Core.Utilities.Results.Concrete.ErrorResults
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message, HttpStatusCode statusCode) : base(message, false, statusCode)
        {
        }
        public ErrorResult(HttpStatusCode statusCode) : base(false, statusCode)
        {
        }
    }
}
