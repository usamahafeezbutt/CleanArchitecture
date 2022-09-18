
using FluentValidation;

namespace CleanArchitecture.Application.Services.Account.Dto.Validators
{
    public class AuthRequestDtoValidator : AbstractValidator<AuthRequestDto>
    {
        public AuthRequestDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
