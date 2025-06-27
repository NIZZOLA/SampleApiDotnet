using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Store.Infra.Data.Sql.Context;

namespace Store.Infra.Data.Sql.Tests;
internal class ContainersFixture : IAsyncLifetime
{
    private StoreContext? _context;
    private IContainer? _container;
    public async Task InitializeAsync()
    {
        _container = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPortBinding(SqlConfigVariables.MsSqlPort, true)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("SQLCMDUSER", SqlConfigVariables.Username)
            .WithEnvironment("SQLCMDPASSWORD", SqlConfigVariables.Password)
            .WithEnvironment("MSSQL_SA_PASSWORD", SqlConfigVariables.Password)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(SqlConfigVariables.MsSqlPort))
            .Build();
        await _container.StartAsync();
        
        var host = _container.Hostname;
        var port = _container.GetMappedPublicPort(SqlConfigVariables.MsSqlPort);

        // Replace connection string in DbContext
        var connectionString = $"Server={host},{port};Database={SqlConfigVariables.Database};User Id={SqlConfigVariables.Username}" +
            $";Password={SqlConfigVariables.Password};TrustServerCertificate=True";

        _context = new StoreContext(
            new DbContextOptionsBuilder<StoreContext>()
                .UseSqlServer(connectionString)
                .Options);

        _context.Database.Migrate();
    }
    public StoreContext GetContext()
    {
        if (_context == null)
        {
            throw new InvalidOperationException("Container not initialized. Call InitializeAsync first.");
        }
        return _context;
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _container.StopAsync();
        await _container.DisposeAsync();
    }
}
