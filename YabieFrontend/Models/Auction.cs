using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using YabieBackend.Models;

namespace YabieFrontend.Models;

public class Auction
{
    [Required]
    [Key]
    [JsonPropertyName("auctionId")]
    public string AuctionId { get; set; }

    [Required]
    [JsonPropertyName("productRefId")]
    public string ProductRefId { get; set; }

    [Required]
    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    [Required]
    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }

    [Required]
    [JsonPropertyName("registrationFee")]
    public decimal RegistrationFee { get; set; }

    [Required]
    [JsonPropertyName("status")]
    public Status Status { get; set; }
}