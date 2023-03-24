using System.Text.Json.Serialization;

namespace MAS_Restaurant.Models
{
    public class DishCard
    {
        [JsonPropertyName("card_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("dish_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("card_descr")]
        public String? Description { get; set; }
        
        [JsonPropertyName("card_time")]
        public double Time { get; set; }

        [JsonPropertyName("equip_type")]
        public int EquipmentTypeId { get; set; }
        
        [JsonPropertyName("operations")]
        public List<Operation>? Operations { get; set; }
        
        public override string ToString()
        {
            var str = $"DishCard with id: { Id }, name: { Name }, says: { Description }\n"
                      + $"Time for cooking: { Time }, using { EquipmentTypeId } equipment type id\n"
                      + "\nUsing following operations:\n";
            var operationsString = Operations?.Aggregate("", (acc, p) => acc + p.ToString()) ?? "";
            if (string.IsNullOrEmpty(operationsString))
            {
                return str + "NONE\n";
            }
            return str + operationsString;
        }
    }
}
