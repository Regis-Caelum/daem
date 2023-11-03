using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace YabieBackend.Models;

[PrimaryKey(nameof(UserId), nameof(AuctionId))]
public record UserAuction
{
    [ForeignKey("Users")] public Guid UserId { get; init; }

    [ForeignKey("Auctions")] public Guid AuctionId { get; init; }
}