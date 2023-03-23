using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerializeJSON
{
    
    public class Cooker
    {
        [JsonPropertyName("cook_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("cook_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("cook_active")]
        public bool IsActive { get; set; }
    }
    
    public class DishCard
    {
        [JsonPropertyName("card_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("dish_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("card_descr")]
        public String? Description { get; set; }
        
        [JsonPropertyName("card_time")]
        public DateTimeOffset Time { get; set; }
        
        [JsonPropertyName("equip_type")]
        public EquipmentType? EquipmentType { get; set; }
        
        [JsonPropertyName("operations")]
        public List<Operation>? Operations { get; set; }
    }
    
    public class EquipmentType
    {
        [JsonPropertyName("equip_type_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("equip_type_name")]
        public String? Name { get; set; }
    }
    
    public class Equipment
    {
        [JsonPropertyName("equip_type")]
        public EquipmentType? Type { get; set; }
        
        [JsonPropertyName("equip_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("equip_active")]
        public bool IsActive { get; set; }
    }
    
    public class OperationType
    {
        [JsonPropertyName("oper_type_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("oper_type_name")]
        public String? Name { get; set; }
    }
    
    public class Operation
    {
        [JsonPropertyName("oper_type")]
        public OperationType? Type { get; set; }
        
        [JsonPropertyName("oper_time")]
        public DateTimeOffset Time { get; set; }
        
        [JsonPropertyName("oper_async_point")]
        public int AsyncPoint { get; set; }
        
        [JsonPropertyName("oper_products")]
        public List<Product>? Products { get; set; } // only type and quantity
    }
    
    public class ProductType
    {
        [JsonPropertyName("prod_type_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("prod_type_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("prod_is_food")]
        public bool IsFood { get; set; }
    }
    
    public class Product
    {
        //[JsonIgnore]
        [JsonPropertyName("prod_item_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("prod_item_type")]
        public ProductType? Type { get; set; }
        
        //[JsonIgnore]
        [JsonPropertyName("prod_item_name")]
        public String? Name { get; set; }
        
        //[JsonIgnore]
        [JsonPropertyName("prod_item_company")]
        public String? Company { get; set; }
        
        //[JsonIgnore]
        [JsonPropertyName("prod_item_unit")]
        public String? Unit { get; set; }
        
        [JsonPropertyName("prod_item_quantity")]
        public int Quantity { get; set; }
        
        //[JsonIgnore]
        [JsonPropertyName("prod_item_cost")]
        public double Cost { get; set; }
        
        //[JsonIgnore]
        [JsonPropertyName("prod_item_delivered")]
        public DateTimeOffset Delivered { get; set; }
        
        //[JsonIgnore]
        [JsonPropertyName("prod_item_valid_until")]
        public DateTimeOffset ValidUntil { get; set; }
    }
    
    public class MenuDish
    {
        [JsonPropertyName("menu_dish_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("menu_dish_card")]
        public DishCard? Card { get; set; }
        
        [JsonPropertyName("menu_dish_price")]
        public double Price { get; set; }
        
        [JsonPropertyName("menu_dish_active")]
        public bool IsActive { get; set; }
    }
    
    public class OrderDish
    {
        [JsonPropertyName("cook_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("cook_id")]
        public MenuDish? MenuDish { get; set; }
    }
    
    public class VisitorOrder
    {
        [JsonPropertyName("cook_id")]
        public String? Name { get; set; }
        
        [JsonPropertyName("cook_id")]
        public DateTimeOffset Started { get; set; }
        
        [JsonPropertyName("cook_id")]
        public DateTimeOffset Ended { get; set; } // for statistics
        
        [JsonPropertyName("cook_id")]
        public double Total { get; set; } // for statistics
        
        [JsonPropertyName("cook_id")]
        public List<OrderDish>? Dishes { get; set; }
    }
    
    /// <summary>
    /// Класс с основной программой.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        /// <param name="args"> Аргументы командной строки </param>
        public static void Main(string[] args)
        {
            // Get the directory containing the MyCode.cs file
            string myCodeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // Get the path to the MyTests.cs file relative to the MyCode.cs file
            string path = Path.Combine(myCodeDir);
            for (int i = 0; i < 3; ++i)
            {
                path = Directory.GetParent(path).FullName;
            }
            Console.WriteLine(path);
            
            string folderPath = Path.Combine(path, "json-txt");
            Console.WriteLine(folderPath);

            var equipmentTypes = ReadJsonFile<EquipmentType>(Path.Combine(folderPath, "equipment_type.txt"));
            var cookers = ReadJsonFile<Cooker>(Path.Combine(folderPath, "cookers.txt"));
            //var products = ReadJsonFile<Product>(Path.Combine(folderPath, "products.txt"));

            // Do something with the deserialized objects
            // ...
            Console.WriteLine();
            foreach (var x in equipmentTypes)
            {
                Console.WriteLine(x?.Id + " " + x?.Name);
            }
            Console.WriteLine();
            foreach (var x in cookers)
            {
                Console.WriteLine(x?.Id + " " + x?.Name + " " + x?.IsActive);
            }
            Console.WriteLine();/*
            foreach (var x in products)
            {
                Console.WriteLine(x?.Id + " " + x?.Name+ " " + x?.Cost + " " + x?.Delivered);
            }
            */
        }
        
        private static List<TElement>? ReadJsonFile<TElement>(string filePath)
        {
            string listPropertyName = Path.GetFileNameWithoutExtension(filePath);
            Console.WriteLine(listPropertyName);
            string jsonString = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonObject = JsonSerializer.Deserialize<Dictionary<string, List<TElement>>>(jsonString, options);
            return jsonObject[listPropertyName];
        }

    }
}
