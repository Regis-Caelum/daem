using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YabieBackend.Models;

public record CartItem
{
    [Required] [Key] public Guid CartItemId { get; init; }
    [Required] [ForeignKey("Cart")] public Guid CartRefId { get; init; }

    [Required]
    [ForeignKey("Products")]
    public Guid ProductRefId { get; init; }

    [Required] public bool Active { get; init; }
    [Required] public uint Quantity { get; init; }
}