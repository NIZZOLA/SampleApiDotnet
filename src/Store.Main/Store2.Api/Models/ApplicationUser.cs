using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Store2.Api.Models;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
}

