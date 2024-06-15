using FluentValidation.TestHelper;
using Store.Application.Constants;
using Store.Application.Contracts;
using Store.Application.Validators;

namespace Store.Application.Tests;

public class StorePostRequestModelValidatorTests
{
    private readonly StorePostRequestModelValidator _validator;

    public StorePostRequestModelValidatorTests()
    {
        _validator = new StorePostRequestModelValidator();
    }

    [Fact]
    public void Should_Have_Error_When_StoreName_Is_Empty()
    {
        var model = new StorePostRequestModel { Name = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(ValidationMessageConstants
              .StoreNameRequired);
    }

    [Fact]
    public void Should_Have_Error_When_StoreName_Exceeds_MaxLength()
    {
        var model = new StorePostRequestModel { Name = new string('a', 51) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(ValidationMessageConstants.StoreNameMaxLength);
    }

    [Fact]
    public void Should_Not_Have_Error_When_StoreName_Is_Valid()
    {
        var model = new StorePostRequestModel { Name = "Valid Store Name" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Empty()
    {
        var model = new StorePostRequestModel { Phone = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
              .WithErrorMessage(ValidationMessageConstants.PhoneRequired);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Exceeds_MaxLength()
    {
        var model = new StorePostRequestModel { Phone = new string('1', 16) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
              .WithErrorMessage(ValidationMessageConstants.PhoneMaxLength);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Phone_Is_Valid()
    {
        var model = new StorePostRequestModel { Phone = "123456789" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Phone);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var model = new StorePostRequestModel { Email = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(ValidationMessageConstants.EmailRequired);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Exceeds_MaxLength()
    {
        var model = new StorePostRequestModel { Email = new string('a', 101) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(ValidationMessageConstants.EmailMaxLength);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Email_Is_Valid()
    {
        var model = new StorePostRequestModel { Email = "test@example.com" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }
}
