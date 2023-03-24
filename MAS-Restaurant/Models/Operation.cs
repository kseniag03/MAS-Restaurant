using System.Text.Json.Serialization;

namespace MAS_Restaurant.Models
{
    public class Operation
    {
        [JsonPropertyName("oper_type")]
        public int OperationTypeId { get; set; }
        
        [JsonPropertyName("oper_time")]
        public double Time { get; set; }
        
        [JsonPropertyName("oper_async_point")]
        public int AsyncPoint { get; set; }
        
        [JsonPropertyName("oper_products")]
        public List<Product>? Products { get; set; }
        
        public override string ToString()
        {
            var str = $"Operation with type: { OperationTypeId }, at async point: { AsyncPoint }\n"
                      + $"Time for operation: { Time }\n"
                      + "\nUsing following products:\n";
            var productsString = Products?.Aggregate("", (acc, p) => acc + p.ToString()) ?? "";
            if (string.IsNullOrEmpty(productsString))
            {
                return str + "NONE\n";
            }
            return str + productsString;
        }
    }
}
