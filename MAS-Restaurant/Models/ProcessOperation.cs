using System.Text.Json.Serialization;

namespace MAS_Restaurant.Models
{
    public class ProcessOperation
    {
        [JsonPropertyName("proc_oper")]
        public int Id { get; set; }
    }
}
