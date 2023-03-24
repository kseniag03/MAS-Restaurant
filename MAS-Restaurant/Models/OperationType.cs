using System.Text.Json.Serialization;

namespace MAS_Restaurant.Models
{
    public class OperationType
    {
        [JsonPropertyName("oper_type_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("oper_type_name")]
        public String? Name { get; set; }
        
        public override string ToString()
        {
            return $"OperationType with id: { Id }, name: { Name }\n";
        }
    }
}