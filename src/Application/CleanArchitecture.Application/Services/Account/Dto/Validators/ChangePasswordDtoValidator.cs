

using CleanArchitecture.Application.Common.Constants.Validations;
using FluentValidation;

namespace CleanArchitecture.Application.Services.Account.Dto.Validators
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.NewPassword)).WithMessage(ValidationConstants.PasswordValueMessage)
                .Must(x => !string.IsNullOrWhiteSpace(x.ConfirmPassword)).WithMessage(ValidationConstants.PasswordValueMessage)
                .Must(x => x.NewPassword.Length >= 6).WithMessage(ValidationConstants.PasswordSizeLimitMessage)
                .Must(x => x.ConfirmPassword.Length >= 6).WithMessage(ValidationConstants.PasswordSizeLimitMessage)
                .Must(x => string.Equals(x.NewPassword, x.ConfirmPassword)).WithMessage(ValidationConstants.NewConfirmPasswordMessage);

        }
    }
}
