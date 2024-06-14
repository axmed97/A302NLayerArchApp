using System.Net;

namespace Core.Utilities.Results.Concrete.SuccessResults
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message, HttpStatusCode statusCode) : base(data, message, true, statusCode)
        {
        }

        public SuccessDataResult(T data, HttpStatusCode statusCode) : base(data, true, statusCode)
        {
        }

        public SuccessDataResult(string message, HttpStatusCode statusCode) : base(default, message, true, statusCode)
        {
        }

        public SuccessDataResult(HttpStatusCode statusCode) : base(default, true, statusCode)
        {
        }
    }
}
