using System.Text.Json.Serialization;

namespace MAS_Restaurant.Models
{
    public class Equipment
    {
        [JsonPropertyName("equip_type")]
        public int EquipmentTypeId { get; set; }
        
        [JsonPropertyName("equip_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("equip_active")]
        public bool IsActive { get; set; }
        
        public override string ToString()
        {
            return $"Equipment with type id: { EquipmentTypeId }, name: { Name } is { (IsActive ? "" : "not ") }active\n";
        }
    }

}