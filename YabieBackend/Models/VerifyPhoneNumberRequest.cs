using System.ComponentModel.DataAnnotations;

namespace YabieBackend.Models;

public record VerifyPhoneNumberRequest
{
    [Required] [Phone] private string PhoneNumber { get; init; }
}