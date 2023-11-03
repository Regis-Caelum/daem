using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YabieBackend.Models;

public record Auction
{
    [Required] [Key] public Guid AuctionId { get; init; }

    [Required]
    [ForeignKey("Products")]
    public Guid ProductRefId { get; init; }

    [Required] public DateTime StartTime { get; init; }
    [Required] public DateTime EndTime { get; init; }
    [Required] public decimal RegistrationFee { get; init; }
    [Required] public Status Status { get; init; }
}