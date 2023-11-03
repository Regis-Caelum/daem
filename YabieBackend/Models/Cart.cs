using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YabieBackend.Models;

public record Cart
{
    [Required] [Key] public Guid CartId { get; init; }
    [Required] [ForeignKey("Users")] public Guid UserRefId { get; init; }
    [Required] public bool Active { get; init; }
}