using eCommerce.Core.Dto;
using FluentValidation;

namespace eCommerce.Core.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            //Email
            RuleFor(temp=> temp.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email Address Format");

            //Pssword
            RuleFor(temp => temp.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
