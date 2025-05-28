using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Store.Infra.Data.Sql.Tests;

public class StoreRepositoryTests
{
    private WebApplicationFactory<Program> _factory;
    public StoreRepositoryTests()
    {

    }

    [Fact]
    public void Test()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/Resident/GetAllResidents");

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var allresidentsResponse = JsonSerializer.Deserialize<GetAllResidentsResponse>(content);

        allresidentsResponse.Should().NotBeNull();
        allresidentsResponse.residents.Should().NotBeNull();
        allresidentsResponse.residents.Should().BeEmpty();
    }
}
