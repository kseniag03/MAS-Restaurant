using System.Text.Json.Serialization;

namespace MAS_Restaurant.Utility;
internal class Log
{
    [JsonPropertyName("log")]
    public string _log { get; set; }

    public void AddLog(string log)
    {
        _log += log + '\n';
    }

    public string GetLog()
    {
        return _log;
    }
}