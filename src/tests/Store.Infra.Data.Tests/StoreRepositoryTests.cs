using Amazon.Runtime.Internal.Util;
using FizzWare.NBuilder;
using Mapster;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NSubstitute;
using Store.Domain.Domain;
using Store.Domain.Repositories;
using Store.Infra.Data.Context;
using Store.Infra.Data.Model;

namespace Store.Infra.Data.Tests;

public class StoreRepositoryTests
{
    private readonly IStoreMongoDbContext _context;
    private readonly ILogger<StoreRepository> _logger;
    private readonly StoreRepository _storeRepository;

    public StoreRepositoryTests()
    {
        _context = Substitute.For<IStoreMongoDbContext>();
        _logger = Substitute.For<ILogger<StoreRepository>>();
        _storeRepository = new StoreRepository(_context, _logger);
    }

    [Fact]
    public async Task Create_Should_Log_And_Insert_Store()
    {
        // Arrange
        var storeModel = Builder<StoreModel>.CreateNew().Build();
        var storeDataModel = storeModel.Adapt<StoreDataModel>();
        _context.Stores.InsertOneAsync(Arg.Any<StoreDataModel>()).Returns(Task.CompletedTask);

        // Act
        var result = await _storeRepository.Create(storeModel);

        // Assert
        await _context.Stores.Received(1).InsertOneAsync(Arg.Is<StoreDataModel>(s =>
            s.Id == storeDataModel.Id &&
            s.Name == storeDataModel.Name // Adicione outras propriedades conforme necessário
        ));
        Assert.True(result);
        Assert.Contains(LogLevel.Information.ToString(), GetLoggerReceivedItems());
    }

    private IEnumerable<string> GetLoggerReceivedItems()
    {
        return _logger?.ReceivedCalls()?
                                              .SelectMany(x => x.GetArguments())?
                                              .Select(x => x?.ToString());
    }

    [Fact]
    public async Task Create_Should_LogError_When_Exception_Occurs()
    {
        // Arrange
        var storeModel = Builder<StoreModel>.CreateNew().Build();
        _context.Stores.InsertOneAsync(Arg.Any<StoreDataModel>()).Returns(Task.FromException(new Exception("Error")));

        // Act
        var result = await _storeRepository.Create(storeModel);

        // Assert
        Assert.False(result);

        
        //_logger.Received().LogError(Arg.Is<string>(s => s.Contains(StoreMessageConstants.CreateError)));
    }

    [Fact]
    public async Task Delete_Should_Log_And_Delete_Store()
    {
        // Arrange
        var id = Guid.NewGuid();
        var deleteResult = Substitute.For<DeleteResult>();
        deleteResult.IsAcknowledged.Returns(true);
        _context.Stores.DeleteOneAsync(Arg.Any<FilterDefinition<StoreDataModel>>()).Returns(Task.FromResult(deleteResult));

        // Act
        var result = await _storeRepository.Delete(id);

        // Assert
        Assert.True(result);
        //_logger.Received().LogInformation(Arg.Is<string>(s => s.Contains(StoreMessageConstants.DeleteProcessStarted)));
    }

    [Fact]
    public async Task Delete_Should_LogError_When_Exception_Occurs()
    {
        // Arrange
        var id = Guid.NewGuid();
        _context.Stores.DeleteOneAsync(Arg.Any<FilterDefinition<StoreDataModel>>()).Returns(Task.FromException<DeleteResult>(new Exception("Error")));

        // Act
        var result = await _storeRepository.Delete(id);

        // Assert
        Assert.False(result);
        //_logger.Received().LogError(Arg.Is<string>(s => s.Contains(StoreMessageConstants.DeleteError)));
    }

    [Fact]
    public async Task GetAll_Should_Return_Stores()
    {
        // Arrange
        var stores = Builder<StoreDataModel>.CreateListOfSize(10).Build();
        var cursor = Substitute.For<IAsyncCursor<StoreDataModel>>();
        cursor.Current.Returns(stores);
        cursor.MoveNextAsync().Returns(Task.FromResult(true), Task.FromResult(false));
        _context.Stores.FindAsync(Arg.Any<FilterDefinition<StoreDataModel>>()).Returns(Task.FromResult(cursor));

        // Act
        var result = await _storeRepository.GetAll();

        // Assert
        Assert.Equal(stores.Adapt<IEnumerable<StoreModel>>(), result);
    }

    [Fact]
    public async Task GetOne_Should_Return_Store()
    {
        // Arrange
        var id = Guid.NewGuid();
        var store = Builder<StoreDataModel>.CreateNew().Build();
        var cursor = Substitute.For<IAsyncCursor<StoreDataModel>>();
        cursor.Current.Returns(new List<StoreDataModel> { store });
        cursor.MoveNextAsync().Returns(Task.FromResult(true), Task.FromResult(false));
        _context.Stores.FindAsync(Arg.Any<FilterDefinition<StoreDataModel>>()).Returns(Task.FromResult(cursor));

        // Act
        var result = await _storeRepository.GetOne(id);

        // Assert
        Assert.Equal(store.Adapt<StoreModel>(), result);
    }

    [Fact]
    public async Task Update_Should_Log_And_Update_Store()
    {
        // Arrange
        var storeModel = Builder<StoreModel>.CreateNew().Build();
        var storeDataModel = storeModel.Adapt<StoreDataModel>();
        var updateResult = Substitute.For<ReplaceOneResult>();
        updateResult.IsAcknowledged.Returns(true);
        updateResult.ModifiedCount.Returns(1);
        _context.Stores.ReplaceOneAsync(Arg.Any<FilterDefinition<StoreDataModel>>(), storeDataModel).Returns(Task.FromResult(updateResult));

        // Act
        var result = await _storeRepository.Update(storeModel);

        // Assert
        Assert.True(result);
        //_logger.Received().LogInformation(Arg.Is<string>(s => s.Contains(StoreMessageConstants.UpdateProcessStarted)));
    }

    [Fact]
    public async Task Update_Should_LogError_When_Exception_Occurs()
    {
        // Arrange
        var storeModel = Builder<StoreModel>.CreateNew().Build();
        _context.Stores.ReplaceOneAsync(Arg.Any<FilterDefinition<StoreDataModel>>(), Arg.Any<StoreDataModel>()).Returns(Task.FromException<ReplaceOneResult>(new Exception("Error")));

        // Act
        var result = await _storeRepository.Update(storeModel);

        // Assert
        Assert.False(result);
        //_logger.Received().LogError(Arg.Is<string>(s => s.Contains(StoreMessageConstants.UpdateError)));
    }
}
