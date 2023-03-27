using System.Collections.Generic;
using System.Text.Json;

namespace MAS_Restaurant
{
    public class JsonSerializer
    {
        public static void WriteJsonFile<TElement>(string filePath, List<TElement> list, string logName)
        {
            var log = new Dictionary<string, List<TElement>> { { logName, list } };

            var jsonString = System.Text.Json.JsonSerializer.Serialize(log, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            File.WriteAllText(filePath, jsonString);
        }

        public static IEnumerable<TElement>? ReadJsonFile<TElement>(string filePath)
        {
            var listPropertyName = Path.GetFileNameWithoutExtension(filePath);
            Console.WriteLine(listPropertyName);
            
            var jsonString = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            try
            {
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<TElement>>>(jsonString, options);

                var list = jsonObject?[listPropertyName];
                if (list is null)
                {
                    return new List<TElement>();
                }
                PrintList(list);

                return list;
            }
            catch
            {
                return new List<TElement>();
            }
        }
        
        public static void PrintList<TElement>(IEnumerable<TElement> list)
        {
            Console.WriteLine();
            foreach (var x in list.Where(x => x != null))
            {
                Console.WriteLine(x);
            }
        }
    }
}
