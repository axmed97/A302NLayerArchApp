using System.Net;

namespace Core.Utilities.Results.Concrete.ErrorResults
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message, HttpStatusCode statusCode) : base(data, message, false, statusCode)
        {
        }

        public ErrorDataResult(T data, HttpStatusCode statusCode) : base(data, false, statusCode)
        {
        }

        public ErrorDataResult(string message, HttpStatusCode statusCode) : base(default, message, false, statusCode)
        {
        }

        public ErrorDataResult(HttpStatusCode statusCode) : base(default, false, statusCode)
        {
        }
    }
}
