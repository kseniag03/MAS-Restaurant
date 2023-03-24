using System.Text.Json;

namespace MAS_Restaurant
{
    public class JsonSerializer
    {
        public static IEnumerable<TElement>? ReadJsonFile<TElement>(string filePath)
        {
            var listPropertyName = Path.GetFileNameWithoutExtension(filePath);
            Console.WriteLine(listPropertyName);
            
            var jsonString = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonObject = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<TElement>>>(jsonString, options);

            var list = jsonObject?[listPropertyName];
            if (list is null)
            {
                return null;
            }
            PrintList(list);
            
            return list;
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
