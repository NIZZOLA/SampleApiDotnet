using System.ComponentModel.DataAnnotations;

namespace Store2.Api.Contracts;

public class CreateUserRequestModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
