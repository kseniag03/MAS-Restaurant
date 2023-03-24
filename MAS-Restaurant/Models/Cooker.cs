using System.Text.Json.Serialization;

namespace MAS_Restaurant.Models
{
    public class Cooker
    {
        [JsonPropertyName("cook_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("cook_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("cook_active")]
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"Cooker with id: { Id }, name: { Name } is { (IsActive ? "" : "not ") }active\n";
        }
    }
}
