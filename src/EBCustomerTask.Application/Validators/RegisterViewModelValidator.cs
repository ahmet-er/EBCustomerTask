using EBCustomerTask.Application.DTOs;
using FluentValidation;

namespace EBCustomerTask.Application.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail gereklidir.")
                .EmailAddress().WithMessage("Geçersiz E-mail adresi.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola gereklidir.")
                .MinimumLength(6).WithMessage("Paralo en az 6 karakter olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Parola doğrulama gereklidir.")
                .Equal(x => x.Password).WithMessage("Parola ve doğrulama parolası eşleşmiyor.");
        }
    }
}
