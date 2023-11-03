using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YabieBackend.Models;

public record Order
{
    [Required] [Key] public Guid OrderId { get; init; }
    [Required] [ForeignKey("Users")] public Guid UserRefId { get; init; }
    [Required] public Status Status { get; init; }
    [Required] public DateTime OrderDate { get; init; }
}