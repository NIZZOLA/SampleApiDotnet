using Microsoft.Extensions.Logging.Testing;
using Store.Domain.Domain;
using Store.Domain.Interfaces.Repositories;
using Store.Infra.Data.Sql.Repositories;

namespace Store.Infra.Data.Sql.Tests;

public class StoreRepositoryTests
{
    private readonly IStoreRepository _storeRepository;
    public StoreRepositoryTests()
    {
        var fixture = new ContainersFixture();
        fixture.InitializeAsync().Wait();
        var dbContext = fixture.GetContext();
       // _storeRepository = new StoreRepository(dbContext, new FakeLogger<StoreRepository>());
    }

    [Fact]
    public async Task CreateNew()
    {
        var newStore = CreateInstance();

        // Act
        var response = await _storeRepository.Create(newStore);

        // Assert
        var product = await _storeRepository.GetOne(Guid.Parse(newStore.Id));

        Assert.NotNull(product);
        Assert.Equal(newStore.Name, product.Name);
    }

    [Fact]
    public async Task Delete()
    {
        var newStore = CreateInstance();

        // Act
        var response = await _storeRepository.Create(newStore);

        // Assert
        var product = await _storeRepository.GetOne(Guid.Parse(newStore.Id));

        Assert.NotNull(product);
        Assert.Equal(newStore.Name, product.Name);

        var deleteResponse = await _storeRepository.Delete(Guid.Parse(newStore.Id));
        Assert.True(deleteResponse);
        var deletedProduct = await _storeRepository.GetOne(Guid.Parse(newStore.Id));
        Assert.Null(deletedProduct);
    }

    [Fact]
    public async Task Update()
    {
        var newStore = CreateInstance();

        // Act
        var response = await _storeRepository.Create(newStore);

        // Assert
        var product = await _storeRepository.GetOne(Guid.Parse(newStore.Id));

        product.Name = "Updated Store Name";
        await _storeRepository.Update(product);
        var updatedProduct = await _storeRepository.GetOne(Guid.Parse(newStore.Id));

        Assert.NotNull(product);
        Assert.Equal(updatedProduct.Name, product.Name);
    }

    private StoreModel CreateInstance()
    {
        return new StoreModel
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Test Store",
            Phone = "1234567890",
            Email = "abc@test.com"
        };
    }
}
