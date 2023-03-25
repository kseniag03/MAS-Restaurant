using System.Text.Json.Serialization;

namespace MAS_Restaurant.Responces
{
    public struct ProcessResponce
    {
        [JsonPropertyName("proc_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("ord_dish")]
        public int OrderDishId { get; set; }
        
        [JsonPropertyName("proc_started")]
        public DateTimeOffset Started { get; set; }
        
        [JsonPropertyName("proc_ended")]
        public DateTimeOffset Ended { get; set; }
        
        [JsonPropertyName("proc_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("proc_operations")]
        public List<ProcessOperationResponce> Operations { get; set; }
        
        public override string ToString()
        {
            var str = $"VisitorOrder with id: { Id }, order dish id: { OrderDishId }, " +
                      $"started at { Started }, ended at { Ended }\n" + 
                      $"\nProcess with following operations id:\n";
            var operationsIdString = Operations?.Aggregate("", (acc, p) => acc + p.ToString()) ?? "";
            if (string.IsNullOrEmpty(operationsIdString))
            {
                return str + "NONE\n";
            }
            return str + operationsIdString;
        }
    }
}