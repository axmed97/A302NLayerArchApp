using FluentValidation;
using System.Net;

namespace Core.CrossCuttingConserns.Validation.FluentValidation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new HttpRequestException(message: result.ToString(), statusCode: HttpStatusCode.BadRequest, inner: new Exception("Validation Exception"));
            }
        }
    }
}
