using System.Text.Json.Serialization;

namespace MAS_Restaurant.Models
{
    public class OrderDish
    {
        [JsonPropertyName("ord_dish_id")]
        public int Id { get; set; }

        [JsonPropertyName("menu_dish")]
        public int MenuDishId { get; set; }
        
        public override string ToString()
        {
            return $"OrderDish with id: { Id }, with menu dish id: { MenuDishId }\n";
        }
    }
}