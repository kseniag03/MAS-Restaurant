using System.Text.Json.Serialization;

namespace MAS_Restaurant.Responces
{
    public struct ProcessOperationResponce
    {
        [JsonPropertyName("proc_oper")]
        public int Id { get; set; }
    }
}