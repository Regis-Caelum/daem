using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YabieBackend.Models;

public record Bid
{
    [Required] [Key] public Guid BidId { get; init; }

    [Required]
    [ForeignKey("Auction")]
    public Guid AuctionRefId { get; init; }

    [Required] 
    [ForeignKey("Users")] 
    public Guid UserRefId { get; init; }
    
    [Required] public decimal BidAmount { get; init; }
    [Required] public long BidTime { get; init; }
}