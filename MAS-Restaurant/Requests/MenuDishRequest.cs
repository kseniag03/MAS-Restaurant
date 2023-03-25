using System.Text.Json.Serialization;

namespace MAS_Restaurant.Requests
{
    public struct MenuDishRequest
    {
        [JsonPropertyName("menu_dish_id")]
        public int Id { get; set; }

        [JsonPropertyName("menu_dish_card")]
        public int DishCardId { get; set; }
        
        [JsonPropertyName("menu_dish_price")]
        public double Price { get; set; }
        
        [JsonPropertyName("menu_dish_active")]
        public bool IsActive { get; set; }
        
        public override string ToString()
        {
            return $"MenuDish with id: { Id }, price: { Price } is { (IsActive ? "" : "not ") }active\n" +
                   $"There is the dish card with id: { DishCardId }\n";
        }
    }
}