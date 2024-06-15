using FluentValidation;
using Store.Domain.Constants;
using Store.Domain.Domain;

namespace Store.Domain.Validatiors;
public class StoreModelValidator : AbstractValidator<StoreModel>
{
    public StoreModelValidator()
    {
        RuleFor(store => store.Id)
            .NotEmpty().WithMessage(ValidationMessages.IdRequired)
            .Matches("^[a-zA-Z0-9-]+$").WithMessage(ValidationMessages.IdInvalid);

        RuleFor(store => store.Name)
            .NotEmpty().WithMessage(ValidationMessages.NameRequired)
            .MaximumLength(50).WithMessage(ValidationMessages.NameTooLong);

        RuleFor(store => store.Phone)
            .NotEmpty().WithMessage(ValidationMessages.PhoneRequired)
            .MaximumLength(15).WithMessage(ValidationMessages.PhoneTooLong);

        RuleFor(store => store.Email)
            .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
            .MaximumLength(100).WithMessage(ValidationMessages.EmailTooLong)
            .EmailAddress().WithMessage(ValidationMessages.EmailInvalid);
    }
}