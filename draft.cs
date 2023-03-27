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

        public override string ToString()
        {
            return $"Cooker with id: { Id }, name: { Name } is { (IsActive ? "" : "not ") }active\n";
        }
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
        public double Time { get; set; }

        /*
        [JsonPropertyName("equip_type")]
        public EquipmentType? EquipmentType { get; set; }
        */
        
        [JsonPropertyName("equip_type")]
        public int EquipmentTypeId { get; set; }
        
        [JsonPropertyName("operations")]
        public List<Operation>? Operations { get; set; }
        
        public override string ToString()
        {
             var str = $"DishCard with id: { Id }, name: { Name }, says: { Description }\n"
                   + $"Time for cooking: { Time }, using { EquipmentTypeId } equipment type id\n"
                   + "\nUsing following operations:\n";
             var operationsString = Operations?.Aggregate("", (acc, p) => acc + p.ToString()) ?? "";
             if (string.IsNullOrEmpty(operationsString))
             {
                 return str + "NONE\n";
             }
             return str + operationsString;
        }
    }
    
    public class EquipmentType
    {
        [JsonPropertyName("equip_type_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("equip_type_name")]
        public String? Name { get; set; }
        
        public override string ToString()
        {
            return $"EquipmentType with id: { Id }, name: { Name }\n";
        }
    }
    
    public class Equipment
    {
        /*
        [JsonPropertyName("equip_type")]
        public EquipmentType? Type { get; set; }
        */
        
        [JsonPropertyName("equip_type")]
        public int EquipmentTypeId { get; set; }
        
        [JsonPropertyName("equip_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("equip_active")]
        public bool IsActive { get; set; }
        
        public override string ToString()
        {
            return $"Equipment with type id: { EquipmentTypeId }, name: { Name } is { (IsActive ? "" : "not ") }active\n";
        }
    }
    
    public class OperationType
    {
        [JsonPropertyName("oper_type_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("oper_type_name")]
        public String? Name { get; set; }
        
        public override string ToString()
        {
            return $"OperationType with id: { Id }, name: { Name }\n";
        }
    }
    
    public class Operation
    {
        /*
        [JsonPropertyName("oper_type")]
        public OperationType? Type { get; set; }
        */
                
        [JsonPropertyName("oper_type")]
        public int OperationTypeId { get; set; }
        
        [JsonPropertyName("oper_time")]
        public double Time { get; set; }
        
        [JsonPropertyName("oper_async_point")]
        public int AsyncPoint { get; set; }
        
        [JsonPropertyName("oper_products")]
        public List<Product>? Products { get; set; } // only type and quantity
        
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
    
    public class ProductType
    {
        [JsonPropertyName("prod_type_id")]
        public int Id { get; set; }
        
        [JsonPropertyName("prod_type_name")]
        public String? Name { get; set; }
        
        [JsonPropertyName("prod_is_food")]
        public bool IsFood { get; set; }
        
        public override string ToString()
        {
            return $"ProductType with id: { Id }, name: { Name } is { (IsFood ? "" : "not ") }food\n";
        }
    }
    
    public class Product
    {
        [JsonPropertyName("prod_item_id")] //[JsonIgnore]
        public int Id { get; set; }
        
        /*
        [JsonPropertyName("prod_item_type")]
        public ProductType? Type { get; set; }
        */
        
        [JsonPropertyName("prod_item_type")]
        public int ProductTypeId { get; set; }
        
        [JsonPropertyName("prod_item_name")] //[JsonIgnore]
        public String? Name { get; set; }
        
        [JsonPropertyName("prod_item_company")] //[JsonIgnore]
        public String? Company { get; set; }
        
        [JsonPropertyName("prod_item_unit")] //[JsonIgnore]
        public String? Unit { get; set; }
        
        [JsonPropertyName("prod_item_quantity")]
        public double Quantity { get; set; }

        [JsonPropertyName("prod_item_cost")] //[JsonIgnore]
        public double Cost { get; set; }
        
        [JsonPropertyName("prod_item_delivered")] //[JsonIgnore]
        public DateTimeOffset Delivered { get; set; }
        
        [JsonPropertyName("prod_item_valid_until")] //[JsonIgnore]
        public DateTimeOffset ValidUntil { get; set; }
        
        public override string ToString()
        {
            return $"Product with id: { Id }, type id: { ProductTypeId }, name: { Name } made in { Company }, measured in { Unit }\n"
                + $"In stock: { Quantity }, cost: { Cost }\n"
                + $"Was delivered: { Delivered }, is valid until: { ValidUntil }\n";
        }
    }
    
    public class MenuDish
    {
        [JsonPropertyName("menu_dish_id")]
        public int Id { get; set; }
        
        /*
        [JsonPropertyName("menu_dish_card")]
        public DishCard? Card { get; set; }
        */
        
        [JsonPropertyName("menu_dish_card")]
        public int DishCardId { get; set; }
        
        [JsonPropertyName("menu_dish_price")]
        public double Price { get; set; }
        
        [JsonPropertyName("menu_dish_active")]
        public bool IsActive { get; set; }
        
        public override string ToString()
        {
            return $"MenuDish with id: { Id }, price: { Price } is { (IsActive ? "" : "not ") }active\n"
                + $"There is the dish card with id: { DishCardId }\n";
        }
    }
    
    public class OrderDish
    {
        [JsonPropertyName("ord_dish_id")]
        public int Id { get; set; }
        
        /*
        [JsonPropertyName("menu_dish")]
        public MenuDish? MenuDish { get; set; }
        */
        
        [JsonPropertyName("menu_dish")]
        public int MenuDishId { get; set; }
        
        public override string ToString()
        {
            return $"OrderDish with id: { Id }, with menu dish id: { MenuDishId }\n";
        }
    }
    
    public class VisitorOrder
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
        public List<OrderDish>? Dishes { get; set; }
        
        public override string ToString()
        {
            var str = $"VisitorOrder with name: { Name }, started at { Started }, ended at { Ended }, check: { Total }\n"
                      + "\nOrder with following dishes:\n";
            var dishesString = Dishes?.Aggregate("", (acc, p) => acc + p.ToString()) ?? "";
            if (string.IsNullOrEmpty(dishesString))
            {
                return str + "NONE\n";
            }
            return str + dishesString;
        }
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
            var codeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(codeDir);
            for (int i = 0; i < 3; ++i)
            {
                path = Directory.GetParent(path).FullName;
            }
            Console.WriteLine(path);
            var folderPath = Path.Combine(path, "json-txt");
            Console.WriteLine(folderPath);
            Console.WriteLine();

            var cookers = ReadJsonFile<Cooker>(Path.Combine(folderPath, "cookers.txt"));
            var dishCards = ReadJsonFile<DishCard>(Path.Combine(folderPath, "dish_cards.txt"));
            var equipments = ReadJsonFile<Equipment>(Path.Combine(folderPath, "equipment.txt"));
            var equipmentTypes = ReadJsonFile<EquipmentType>(Path.Combine(folderPath, "equipment_type.txt"));
            var menuDishes = ReadJsonFile<MenuDish>(Path.Combine(folderPath, "menu_dishes.txt"));
            var operationTypes = ReadJsonFile<OperationType>(Path.Combine(folderPath, "operation_types.txt"));
            var productTypes = ReadJsonFile<ProductType>(Path.Combine(folderPath, "product_types.txt"));
            var products = ReadJsonFile<Product>(Path.Combine(folderPath, "products.txt"));
            var visitorsOrders = ReadJsonFile<VisitorOrder>(Path.Combine(folderPath, "visitors_orders.txt"));
            
        }
        
        private static IEnumerable<TElement>? ReadJsonFile<TElement>(string filePath)
        {
            var listPropertyName = Path.GetFileNameWithoutExtension(filePath);
            Console.WriteLine(listPropertyName);
            
            var jsonString = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var jsonObject = JsonSerializer.Deserialize<Dictionary<string, List<TElement>>>(jsonString, options);

            var list = jsonObject?[listPropertyName];
            if (list is null)
            {
                return null;
            }
            PrintList(list);
            
            return list;
        }
        
        private static void PrintList<TElement>(List<TElement> list)
        {
            Console.WriteLine();
            foreach (var x in list)
            {
                Console.WriteLine(x);
            }
        }

    }
}
