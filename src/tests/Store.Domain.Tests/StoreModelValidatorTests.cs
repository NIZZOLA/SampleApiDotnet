using FizzWare.NBuilder;
using Store.Domain.Constants;
using Store.Domain.Domain;
using Store.Domain.Validatiors;
using FluentValidation.TestHelper;
using Store.Domain.Tests.Builder;

namespace Store.Domain.Tests;

public class StoreModelValidatorTests
{
    private readonly StoreModelValidator _validator;

    public StoreModelValidatorTests()
    {
        _validator = new StoreModelValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Id = string.Empty).Build();
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Id).WithErrorMessage(ValidationMessages.IdRequired);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Id_Is_Valid()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Id = "123-abc").Build();
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Name = string.Empty).Build();
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage(ValidationMessages.NameRequired);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Long()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Name = new string('a', 51)).Build();
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name).WithErrorMessage(ValidationMessages.NameTooLong);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Name_Is_Valid()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Name = "Valid Name").Build();
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Empty()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Phone = string.Empty).Build();
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Phone).WithErrorMessage(ValidationMessages.PhoneRequired);
    }

    [Fact]
    public void Should_Have_Error_When_Phone_Is_Too_Long()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Phone = new string('1', 16)).Build();
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Phone).WithErrorMessage(ValidationMessages.PhoneTooLong);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Phone_Is_Valid()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Phone = "1234567890").Build();
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Phone);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Email = string.Empty).Build();
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage(ValidationMessages.EmailRequired);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Too_Long()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Email = new string('a', 101) + "@example.com").Build();
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage(ValidationMessages.EmailTooLong);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Email = "invalid-email").Build();
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email).WithErrorMessage(ValidationMessages.EmailInvalid);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Email_Is_Valid()
    {
        var model = Builder<StoreModel>.CreateNew().With(x => x.Email = "test@example.com").Build();
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_Validate_Entity_Successfully()
    {
        var model = Builder<StoreModel>.CreateNew()
            .With(x => x.Id = "123-abc")
            .With(x => x.Name = "Valid Name")
            .With(x => x.Phone = "1234567890")
            .With(x => x.Email = "test@example.com")
            .Build();

        var result = _validator.TestValidate(model);
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Fail_Entity_Validation_When_Fields_Are_Invalid()
    {
        var model = Builder<StoreModel>.CreateNew()
            .With(x => x.Id = string.Empty)
            .With(x => x.Name = new string('a', 51))
            .With(x => x.Phone = new string('1', 16))
            .With(x => x.Email = "invalid-email")
            .Build();

        var result = _validator.TestValidate(model);
        Assert.False(result.IsValid);
    }
}