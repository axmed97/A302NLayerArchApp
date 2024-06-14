using Entities.DTOs.AuthDTOs;
using FluentValidation;
using System.Globalization;

namespace Business.Validations.FluentValidation
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(GetTranslation("FirstnameIsRequired"))
                .NotNull().WithMessage(GetTranslation("FirstnameIsRequired"));

        }

        private string GetTranslation(string key)
        {
            return ValidatorOptions.Global.LanguageManager.GetString(key, new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name));
        }
    }
}
