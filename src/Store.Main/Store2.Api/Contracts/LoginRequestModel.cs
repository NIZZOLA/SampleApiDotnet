using System.ComponentModel.DataAnnotations;

namespace Store2.Api.Contracts;

public class LoginRequestModel
{
    public string Email { get; set; }

    public string Password { get; set; }

}
