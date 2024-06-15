using FluentValidation;
using Store.Application.Constants;

namespace Store.Application.Validators;
public class StorePostRequestModelValidator : AbstractValidator<StorePostRequestModel>
{
    public StorePostRequestModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessageConstants.StoreNameRequired)
            .MaximumLength(50).WithMessage(ValidationMessageConstants.StoreNameMaxLength);

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(ValidationMessageConstants.PhoneRequired)
            .MaximumLength(15).WithMessage(ValidationMessageConstants.PhoneMaxLength);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessageConstants.EmailRequired)
            .MaximumLength(100).WithMessage(ValidationMessageConstants.EmailMaxLength);
    }
}
