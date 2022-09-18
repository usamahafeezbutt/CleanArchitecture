

using CleanArchitecture.Application.Common.Constants.Validations;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Application.Services.Account.Dto.Validators
{
    public class RegistrationRequestDtoValidator : AbstractValidator<RegistrationRequestDto>
    {
        public RegistrationRequestDtoValidator()
        {
            RuleFor(x => x.Email).Must(x => ValidateEmailAddress(x));
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationConstants.NameValueMessage);
            RuleFor(x => x.Password).NotEmpty().Must(x => x.Length >= 6).WithMessage(ValidationConstants.PasswordSizeLimitMessage);
            RuleFor(x => x.Password.Length).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }

        public static bool ValidateEmailAddress(string email)
        => Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase); 
        
    }
}
