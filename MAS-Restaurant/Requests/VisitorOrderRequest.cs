using System.Text.Json.Serialization;

namespace MAS_Restaurant.Requests
{
    public struct VisitorOrderRequest
    {
        [JsonPropertyName("vis_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("vis_ord_started")]
        public DateTimeOffset Started { get; set; }
        
        [JsonPropertyName("vis_ord_ended")]
        public DateTimeOffset Ended { get; set; } // for statistics
        
        [JsonPropertyName("vis_ord_total")]
        public double Total { get; set; } // for statistics
        
        [JsonPropertyName("vis_ord_dishes")]
        public List<OrderDishRequest>? Dishes { get; set; }
        
        public override string ToString()
        {
            var str = $"VisitorOrder with name: { Name }, started at { Started }, ended at { Ended }, " +
                      $"check: { Total }\n\nOrder with following dishes:\n";
            var dishesString = Dishes?.Aggregate("", (acc, p) => acc + p.ToString()) ?? "";
            if (string.IsNullOrEmpty(dishesString))
            {
                return str + "NONE\n";
            }
            return str + dishesString;
        }
    }
}