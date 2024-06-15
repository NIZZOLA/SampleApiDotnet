using FizzWare.NBuilder;
using FluentResults;
using Mapster;
using NSubstitute;
using Store.Application.Contracts;
using Store.Application.Services;
using Store.Application.Validators;
using Store.Business.Interfaces;
using Store.Domain.Domain;

namespace Store.Application.Tests;
public class StoreAppServiceTests
{
    private readonly IStoreService _storeService;
    private readonly StoreAppService _storeAppService;

    public StoreAppServiceTests()
    {
        _storeService = Substitute.For<IStoreService>();
        _storeAppService = new StoreAppService(_storeService);
    }

    [Fact]
    public async Task GetAll_ShouldReturnStoreResponseModels()
    {
        // Arrange
        var stores = Builder<StoreModel>.CreateListOfSize(5)
                    .All()
                    .With(x => x.Id = Guid.NewGuid().ToString())
                    .Build();
        _storeService.GetAll().Returns(Task.FromResult((IEnumerable<StoreModel>)stores));

        // Act
        var result = await _storeAppService.GetAll();

        // Assert
        Assert.NotNull( result);
        Assert.Equal( 5, result.Count);
    }

    [Fact]
    public async Task GetOne_ShouldReturnStoreResponseModel()
    {
        // Arrange
        var store = Builder<StoreModel>.CreateNew().With(x => x.Id = Guid.NewGuid().ToString()).Build();
        // Arrange
        
        _storeService.GetOne(Arg.Any<Guid>()).Returns(Task.FromResult(store));

        // Act
        var result = await _storeAppService.GetOne(Guid.NewGuid());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Save_ShouldReturnValidationError()
    {
        // Arrange
        var invalidModel = new StorePostRequestModel();
        var validator = new StorePostRequestModelValidator();
       
        // Act
        var result = await _storeAppService.Save(invalidModel);

        // Assert
        Assert.True(result.IsFailed);
    }

    public async Task Update_ShouldReturnFail_WhenUpdateFails()
    {
        // Arrange
        string expectedErrorMessage = "";

        var id = Guid.NewGuid();
        var requestModel = new StorePutRequestModel
        {
            Name = "Valid Name",
            Phone = "1234567890",
            Email = "test@example.com"
        };

        var storeModel = requestModel.Adapt<StoreModel>();
        storeModel.Id = id.ToString();

        var errorMessage = "Update operation failed";
        var errors = new List<Error> { new Error(errorMessage) };

        _storeService.Update(storeModel).Returns(Result.Fail<StoreModel>(errors));

        // Act
        var result = await _storeAppService.Update(id, requestModel);

        // Assert
        Assert.True( result.IsFailed);
        Assert.Equal(result.Errors[0].Message, expectedErrorMessage);
    }

    [Fact]
    public async Task Save_ShouldReturnStoreResponseModel()
    {
        // Arrange
        var validModel = new StorePostRequestModel() {  Email = "test@mail.com", Name = "my name test", Phone = "12345689" };
        var storeModel = validModel.Adapt<StoreModel>();
        var store = Builder<StoreModel>.CreateNew()
                        .With(x => x.Id = Guid.NewGuid().ToString())
                        .With(x => x.Name = validModel.Name)
                        .With(x => x.Email = validModel.Email)
                        .With(x => x.Phone = validModel.Phone)
                        .Build();
        var result = Result.Ok(store);

        _storeService.Save(Arg.Any<StoreModel>()).Returns(Task.FromResult(result));

        // Act
        var saveResult = await _storeAppService.Save(validModel);

        // Assert
        Assert.True(saveResult.IsSuccess);
        Assert.NotNull(saveResult.Value);
    }

    [Fact]
    public async Task Update_ShouldReturnValidationError()
    {
        // Arrange
        var invalidModel = new StorePutRequestModel(); // invalid model

        // Act
        var result = await _storeAppService.Update(Guid.NewGuid(), invalidModel);

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public async Task Save_ShouldReturnFail_WhenSaveFails()
    {
        // Arrange
        var requestModel = new StorePostRequestModel
        {
            Name = "Valid Name",
            Phone = "1234567890",
            Email = "test@example.com"
        };

        var storeModel = requestModel.Adapt<StoreModel>();
        storeModel.Id = Guid.NewGuid().ToString();

        var expectedErrorMessage = "Save operation failed";
        var errors = new List<Error> { new Error(expectedErrorMessage) };

        var expectedBusinessError = new Result<StoreModel>().WithError(expectedErrorMessage);
        _storeService.Save(storeModel).Returns(expectedBusinessError);

        // Act
        var result = await _storeAppService.Save(requestModel);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal(result.Errors[0].Message, expectedErrorMessage);
    }

    [Fact]
    public async Task Update_ShouldReturnStoreResponseModel()
    {
        // Arrange
        var validModel = new StorePutRequestModel() { Id = Guid.NewGuid(), Email = "test@mail.com", Name = "my name test", Phone = "12345689" };
        var storeModel = validModel.Adapt<StoreModel>();
        
        var result = Result.Ok(storeModel);

        _storeService.Update(Arg.Any<StoreModel>()).Returns(Task.FromResult(result));

        // Act
        var updateResult = await _storeAppService.Update(validModel.Id, validModel);

        // Assert
        Assert.True(updateResult.IsSuccess);
        Assert.NotNull(updateResult.Value);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFoundError()
    {
        // Arrange
        _storeService.GetOne(Arg.Any<Guid>()).Returns(Task.FromResult<StoreModel>(null));

        // Act
        var result = await _storeAppService.Delete(Guid.NewGuid());

        // Assert
        Assert.True(result.IsFailed);
    }

    [Fact]
    public async Task Delete_ShouldReturnFail_WhenItemNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedErrorMessage = $"Item not found with id: {id}";
        _storeService.GetOne(id).Returns(Task.FromResult<StoreModel>(null));

        // Act
        var result = await _storeAppService.Delete(id);

        // Assert
        Assert.True( result.IsFailed);
        Assert.Equal( result.Errors[0].Message, expectedErrorMessage);
    }

    [Fact]
    public async Task Delete_ShouldReturnSuccess()
    {
        // Arrange
        var store = Builder<StoreModel>.CreateNew().Build();
        var deleteResult = Result.Ok(true);

        _storeService.GetOne(Arg.Any<Guid>()).Returns(Task.FromResult(store));
        _storeService.Delete(Arg.Any<Guid>()).Returns(Task.FromResult(deleteResult));

        // Act
        var result = await _storeAppService.Delete(Guid.NewGuid());

        // Assert
        Assert.True( result.IsSuccess);
    }
}
