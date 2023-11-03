using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YabieBackend.Models;

public record Payment
{
    [Required] [Key] public Guid PaymentId { get; init; }
    [Required] [ForeignKey("Users")] public Guid UserRefId { get; init; }
    [Required] [ForeignKey("Orders")] public Guid OrderRefId { get; init; }
    [Required] public Status Status { get; init; }
    [Required] public PaymentMode Mode { get; init; }
    [Required] public decimal Amount { get; init; }
    [Required] public DateTime PaymentDate { get; init; }
}