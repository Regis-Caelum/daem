using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YabieBackend.Models;

public record LoginRequest
{
    [Required] [Phone] private string PhoneNumber { get; init; }
    [Required] [PasswordPropertyText] private string Password { get; init; }
}