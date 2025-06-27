using Microsoft.Extensions.Logging;
using Store.Domain.Interfaces.Repositories;
using Store.Infra.Data.Sql.Context;

namespace Store.Infra.Data.Sql.Repositories;
public class StoreRepository : BaseRepository, IStoreRepository
{
    private readonly ILogger _logger;
    public StoreRepository(StoreContext context, ILogger<BaseRepository> logger): base(context,logger)
    {
        _logger = logger;
    }
}
