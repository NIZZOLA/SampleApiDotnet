using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Store2.Api.Models;

[CollectionName("Roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{

}
