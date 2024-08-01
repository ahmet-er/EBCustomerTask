using EBCustomerTask.Application.DTOs;
using FluentValidation;

namespace EBCustomerTask.Application.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail gereklidir.")
                .EmailAddress().WithMessage("Geçersiz E-mail adresi.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola gereklidir.");
        }
    }
}
