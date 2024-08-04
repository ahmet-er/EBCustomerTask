using EBCustomerTask.Application.DTOs;
using FluentValidation;

namespace EBCustomerTask.Application.Validators
{
	public class CustomerUpdateViewModelValidator : AbstractValidator<CustomerUpdateViewModel>
	{
        public CustomerUpdateViewModelValidator()
        {
			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("İsim boş bırakılamaz.")
				.MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir.");

			RuleFor(x => x.LastName)
				.NotEmpty().WithMessage("İsim boş bırakılamaz.")
				.MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir.");

			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email boş bırakılamaz.")
				.MaximumLength(100).WithMessage("Email en fazla 100 karakter olabilir.")
				.EmailAddress().WithMessage("Email adresi geçersiz.");

			RuleFor(x => x.PhoneNumber)
				.NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
				.Length(10).WithMessage("Telefon numarası 10 karakter olmalıdır.");

			RuleFor(x => x.BirthDate)
				.NotEmpty().WithMessage("Doğum günü boş bırakılamaz.");

			RuleFor(x => x.PhotoUrl)
				.MaximumLength(250).WithMessage("PhotoUrl en fazla 250 karakter olabilir.");
		}
    }
}
