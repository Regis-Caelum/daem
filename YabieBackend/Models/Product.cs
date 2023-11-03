using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YabieBackend.Models;

public record Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ProductId { get; init; }

    [Required] [StringLength(150)] public string Name { get; init; }
    [Required] [StringLength(500)] public string Description { get; init; }
    [Required] public string ImageUrl { get; init; }
    [Required] public decimal StartingPrice { get; init; }
    [Required] public decimal OriginalPrice { get; init; }
}