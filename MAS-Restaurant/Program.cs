using MAS_Restaurant.Models;

using System.IO;
using System.Reflection;

namespace MAS_Restaurant
{

    public static class FileSettings
    {
        public const string folderTxt = "json-txt";
        public const string folderJson = "json-json";
        public const string extensionTxt = "*.txt";
        public const string extensionJson = "*.json";
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
            var folderPath = Path.Combine(path, FileSettings.folderJson);
            Console.WriteLine(folderPath);
            Console.WriteLine();

/*
            var cookers = JsonSerializer.ReadJsonFile<MAS_Restaurant.Models.Cooker>(Path.Combine(folderPath, "cookers.txt"));
            var dishCards = JsonSerializer.ReadJsonFile<MAS_Restaurant.Models.DishCard>(Path.Combine(folderPath, "dish_cards.txt"));
            var equipments = JsonSerializer.ReadJsonFile<MAS_Restaurant.Models.Equipment>(Path.Combine(folderPath, "equipment.txt"));
            var equipmentTypes = JsonSerializer.ReadJsonFile<MAS_Restaurant.Models.EquipmentType>(Path.Combine(folderPath, "equipment_type.txt"));
            var menuDishes = JsonSerializer.ReadJsonFile<MAS_Restaurant.Models.MenuDish>(Path.Combine(folderPath, "menu_dishes.txt"));
            var operationTypes = JsonSerializer.ReadJsonFile<MAS_Restaurant.Models.OperationType>(Path.Combine(folderPath, "operation_types.txt"));
            var productTypes = JsonSerializer.ReadJsonFile<MAS_Restaurant.Models.ProductType>(Path.Combine(folderPath, "product_types.txt"));
            var products = JsonSerializer.ReadJsonFile<MAS_Restaurant.Models.Product>(Path.Combine(folderPath, "products.txt"));
            var visitorsOrders = JsonSerializer.ReadJsonFile<MAS_Restaurant.Models.VisitorOrder>(Path.Combine(folderPath, "visitors_orders.txt"));
*/          

            string[] files = Directory.GetFiles(folderPath, FileSettings.extensionJson);

            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string filePath = Path.Combine(folderPath, file);
        
                switch (fileName)
                {
                    case "cookers":
                        var cookers = JsonSerializer.ReadJsonFile<Cooker>(filePath);
                        // do something with cookers
                        break;
                    case "dish_cards":
                        var dishCards = JsonSerializer.ReadJsonFile<DishCard>(filePath);
                        // do something with dishCards
                        break;
                    case "equipment":
                        var equipments = JsonSerializer.ReadJsonFile<Equipment>(filePath);
                        // do something with equipments
                        break;
                    case "equipment_type":
                        var equipmentTypes = JsonSerializer.ReadJsonFile<EquipmentType>(filePath);
                        // do something with equipmentTypes
                        break;
                    case "menu_dishes":
                        var menuDishes = JsonSerializer.ReadJsonFile<MenuDish>(filePath);
                        // do something with menuDishes
                        break;
                    case "operation_types":
                        var operationTypes = JsonSerializer.ReadJsonFile<OperationType>(filePath);
                        // do something with operationTypes
                        break;
                    case "product_types":
                        var productTypes = JsonSerializer.ReadJsonFile<ProductType>(filePath);
                        // do something with productTypes
                        break;
                    case "products":
                        var products = JsonSerializer.ReadJsonFile<Product>(filePath);
                        // do something with products
                        break;
                    case "visitors_orders":
                        var visitorsOrders = JsonSerializer.ReadJsonFile<VisitorOrder>(filePath);
                        // do something with visitorsOrders
                        break;
                    default:
                        Console.WriteLine("!!! no class model found !!!");
                        // handle unknown file names
                        break;
                }
            }


            var p = new Process
            {
                Id = 12,
                OrderDishId = 625,
                Started = default,
                Ended = default,
                IsActive = false,
                Operations = new List<ProcessOperation>
                {
                    new ProcessOperation
                    {
                        Id = 1
                    },
                    new ProcessOperation
                    {
                        Id = 17
                    }
                }
            };

            var processList = new List<Process>();
            processList.Add(p);
            
            JsonSerializer.WriteJsonFile(Path.Combine(folderPath, "process_log.json"), processList, "process_log");
        }

    }
}