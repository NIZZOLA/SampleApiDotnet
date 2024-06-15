using FluentValidation.TestHelper;
using Store.Application.Constants;
using Store.Application.Contracts;
using Store.Application.Validators;

namespace Store.Application.Tests;

public class StorePutRequestModelValidatorTests
{
    private readonly StorePutRequestModelValidator _validator;

    public StorePutRequestModelValidatorTests()
    {
        _validator = new StorePutRequestModelValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var model = new StorePutRequestModel { Id = Guid.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage(ValidationMessageConstants.IdRequired);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Id_Is_Valid()
    {
        var model = new StorePutRequestModel { Id = Guid.NewGuid() };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    // Testes herdados de StorePostRequestModel
    [Fact]
    public void Should_Have_Error_When_StoreName_Is_Empty()
    {
        var model = new StorePutRequestModel { Name = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(ValidationMessageConstants.StoreNameRequired);
    }

    [Fact]
    public void Should_Have_Error_When_StoreName_Exceeds_MaxLength()
    {
        var model = new StorePutRequestModel { Name = new string('a', 51) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage(ValidationMessageConstants.StoreNameMaxLength);
    }

    [Fact]
    public void Should_Not_Have_Error_When_StoreName_Is_Valid()
    {
        var model = new StorePutRequestModel { Name = "Valid Store Name" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Empty()
    {
        var model = new StorePutRequestModel { Phone = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
              .WithErrorMessage(ValidationMessageConstants.PhoneRequired);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Exceeds_MaxLength()
    {
        var model = new StorePutRequestModel { Phone = new string('1', 16) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Phone)
              .WithErrorMessage(ValidationMessageConstants.PhoneMaxLength);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Phone_Is_Valid()
    {
        var model = new StorePutRequestModel { Phone = "123456789" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Phone);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var model = new StorePutRequestModel { Email = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(ValidationMessageConstants.EmailRequired);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Exceeds_MaxLength()
    {
        var model = new StorePutRequestModel { Email = new string('a', 101) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage(ValidationMessageConstants.EmailMaxLength);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Email_Is_Valid()
    {
        var model = new StorePutRequestModel { Email = "test@example.com" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }
}
