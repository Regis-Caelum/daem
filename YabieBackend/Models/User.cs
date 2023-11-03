using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YabieBackend.Models;

public record User
{
    [Required] [Key] public Guid UserId { get; init; }
    [Required] [Phone] public string PhoneNumber { get; init; }
    [Required] [EmailAddress] public string Email { get; init; }
    [Required] [StringLength(50)] public string FirstName { get; init; }
    [Required] [StringLength(50)] public string LastName { get; init; }
    [Required] [StringLength(100)] public string Address { get; init; }
    [Required] [PasswordPropertyText] public string Password { get; init; }
    [Required] public DateTime DateOfBirth { get; init; }
    [Required] public DateTime RegistrationDate { get; init; }
}