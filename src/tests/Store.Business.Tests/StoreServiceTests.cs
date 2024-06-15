using FizzWare.NBuilder;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Store.Business.Services;
using Store.Domain.Domain;
using Store.Infra.Data.Interfaces;

namespace Store.Business.Tests;

public class StoreServiceTests
{
    private readonly IStoreRepository _storeRepository;
    private readonly ILogger<StoreService> _logger;
    private readonly StoreService _storeService;

    public StoreServiceTests()
    {
        _storeRepository = Substitute.For<IStoreRepository>();
        _logger = Substitute.For<ILogger<StoreService>>();
        _storeService = new StoreService(_storeRepository, _logger);
    }

    [Fact]
    public async Task Delete_Should_Call_StoreRepository_Delete()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        await _storeService.Delete(id);

        // Assert
        await _storeRepository.Received(1).Delete(id);
    }

    [Fact]
    public async Task GetAll_Should_Call_StoreRepository_GetAll()
    {
        // Arrange
        var stores = Builder<StoreModel>.CreateListOfSize(10).Build();
        _storeRepository.GetAll().Returns(Task.FromResult<IEnumerable<StoreModel>>(stores));

        // Act
        var result = await _storeService.GetAll();

        // Assert
        await _storeRepository.Received(1).GetAll();
        Assert.Equal(stores, result);
    }

    [Fact]
    public async Task GetOne_Should_Call_StoreRepository_GetOne()
    {
        // Arrange
        var id = Guid.NewGuid();
        var store = Builder<StoreModel>.CreateNew().Build();
        _storeRepository.GetOne(id).Returns(Task.FromResult(store));

        // Act
        var result = await _storeService.GetOne(id);

        // Assert
        await _storeRepository.Received(1).GetOne(id);
        Assert.Equal(store, result);
    }

    [Fact]
    public async Task Save_Should_Validate_And_Create_New_Store()
    {
        // Arrange
        var store = Builder<StoreModel>.CreateNew().With(s => s.Id = null).Build();
        var validator = Substitute.For<IValidator<StoreModel>>();
        validator.Validate(store).Returns(new ValidationResult());

        // Act
        var result = await _storeService.Save(store);

        // Assert
        await _storeRepository.Received(1).Create(store);
        Assert.True(result.IsSuccess);
        Assert.False(string.IsNullOrEmpty(store.Id));
    }

    [Fact]
    public async Task Save_Should_Return_Failure_If_Validation_Fails()
    {
        // Arrange
        var store = Builder<StoreModel>.CreateNew().Build();
        var validator = Substitute.For<IValidator<StoreModel>>();
        var validationFailures = new List<ValidationFailure> { new ValidationFailure("Property", "Error") };
        validator.Validate(store).Returns(new ValidationResult(validationFailures));

        // Act
        var result = await _storeService.Save(store);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error", result.Errors.ToString());
    }

    [Fact]
    public async Task Update_Should_Call_StoreRepository_Update()
    {
        // Arrange
        var store = Builder<StoreModel>.CreateNew().Build();

        // Act
        var result = await _storeService.Update(store);

        // Assert
        await _storeRepository.Received(1).Update(store);
        Assert.True(result.IsSuccess);
    }
}