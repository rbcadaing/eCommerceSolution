using eCommerce.Core.Dto;
using FluentValidation;

namespace eCommerce.Core.Validators
{
    public  class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            //Email
            RuleFor(temp => temp.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email Address Format");

            //Pssword
            RuleFor(temp => temp.Password)
                .NotEmpty().WithMessage("Password is required");

            RuleFor(temp => temp.PersonName)
                .NotEmpty().WithMessage("PersonName is Required")
                .Length(1, 50).WithMessage("Person Name should be 1 to 50 characters long");

            RuleFor(temp => temp.Gender).IsInEnum().WithMessage("Invalid gender option");

        }
    }
}
