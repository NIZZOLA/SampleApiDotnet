using FluentValidation;
using Store.Application.Constants;

namespace Store.Application.Validators;
public class StorePutRequestModelValidator : AbstractValidator<StorePutRequestModel>
{
    public StorePutRequestModelValidator()
    {
        Include(new StorePostRequestModelValidator());

        RuleFor(x => x.Id)
            .NotEmpty().WithMessage(ValidationMessageConstants.IdRequired);
    }
}
