using System.Text.Json.Serialization;

namespace MAS_Restaurant.Models
{
    public class ProductType
    {
        [JsonPropertyName("prod_type_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("prod_type_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("prod_is_food")]
        public bool IsFood { get; set; }
        
        public override string ToString()
        {
            return $"ProductType with id: { Id }, name: { Name } is { (IsFood ? "" : "not ") }food\n";
        }
    }
}