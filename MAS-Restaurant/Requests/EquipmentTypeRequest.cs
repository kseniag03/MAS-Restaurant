using System.Text.Json.Serialization;

namespace MAS_Restaurant.Requests
{
    public struct EquipmentTypeRequest
    {
        [JsonPropertyName("equip_type_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("equip_type_name")]
        public String? Name { get; set; }
        
        public override string ToString()
        {
            return $"EquipmentType with id: { Id }, name: { Name }\n";
        }
    }
}
