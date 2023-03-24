using System.Text.Json.Serialization;

namespace MAS_Restaurant.Models
{
    public class Product
    {
        [JsonPropertyName("prod_item_id")]
        public int Id { get; set; }

        [JsonPropertyName("prod_item_type")]
        public int ProductTypeId { get; set; }
        
        [JsonPropertyName("prod_item_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("prod_item_company")]
        public String? Company { get; set; }
        
        [JsonPropertyName("prod_item_unit")]
        public String? Unit { get; set; }
        
        [JsonPropertyName("prod_item_quantity")]
        public double Quantity { get; set; }

        [JsonPropertyName("prod_item_cost")]
        public double Cost { get; set; }
        
        [JsonPropertyName("prod_item_delivered")]
        public DateTimeOffset Delivered { get; set; }
        
        [JsonPropertyName("prod_item_valid_until")]
        public DateTimeOffset ValidUntil { get; set; }
        
        public override string ToString()
        {
            return $"Product with id: { Id }, type id: { ProductTypeId }, name: { Name } made in { Company }, measured in { Unit }\n"
                   + $"In stock: { Quantity }, cost: { Cost }\n"
                   + $"Was delivered: { Delivered }, is valid until: { ValidUntil }\n";
        }
    }
}