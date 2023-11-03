using System.Text.Json.Serialization;

namespace YabieFrontend.Models;

public class Product
{
    [JsonPropertyName("productId")]
    public Guid ProductId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
        
    [JsonPropertyName("description")]
    public string Description { get; set; }
        
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }
        
    [JsonPropertyName("startingPrice")]
    public decimal StartingPrice { get; set; }

    [JsonPropertyName("originalPrice")]
    public decimal OriginalPrice { get; set; }
}