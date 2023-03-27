using MAS_Restaurant.Agents;
using MAS_Restaurant.Requests;
using MAS_Restaurant.Responces;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant
{
    public static class FileSettings
    {
        public const string folderTxt = "json-txt";
        public const string extensionTxt = "*.txt";
        public const string folderJsonInput = "json-json-input";
        public const string folderJsonOutput = "json-json-output";
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
            var folderPath = Path.Combine(path, FileSettings.folderJsonInput);
            Console.WriteLine(folderPath);
            Console.WriteLine();

            string[] files = Directory.GetFiles(folderPath, FileSettings.extensionJson);

            /*
            IEnumerable<CookerRequest> cookers = Enumerable.Empty<CookerRequest>();
            IEnumerable<DishCardRequest> dishCards = Enumerable.Empty<DishCardRequest>();
            IEnumerable<EquipmentRequest> equipments = Enumerable.Empty<EquipmentRequest>();
            IEnumerable<EquipmentTypeRequest> equipmentTypes = Enumerable.Empty<EquipmentTypeRequest>();
            IEnumerable<MenuDishRequest> menuDishes = Enumerable.Empty<MenuDishRequest>();
            IEnumerable<OperationTypeRequest> operationTypes = Enumerable.Empty<OperationTypeRequest>();
            IEnumerable<ProductTypeRequest> productTypes = Enumerable.Empty<ProductTypeRequest>();
            IEnumerable<ProductRequest> products = Enumerable.Empty<ProductRequest>();
            IEnumerable<VisitorOrderRequest> visitorsOrders = Enumerable.Empty<VisitorOrderRequest>();*/
            
            
            var cookers = Enumerable.Empty<CookerRequest>();
            var dishCards = Enumerable.Empty<DishCardRequest>();
            var equipments = Enumerable.Empty<EquipmentRequest>();
            var equipmentTypes = Enumerable.Empty<EquipmentTypeRequest>();
            var menuDishes = Enumerable.Empty<MenuDishRequest>();
            var operationTypes = Enumerable.Empty<OperationTypeRequest>();
            var productTypes = Enumerable.Empty<ProductTypeRequest>();
            var products = Enumerable.Empty<ProductRequest>();
            var visitorsOrders = Enumerable.Empty<VisitorOrderRequest>();
            
            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string filePath = Path.Combine(folderPath, file);
        
                switch (fileName)
                {
                    case "cookers":
                        cookers = JsonSerializer.ReadJsonFile<CookerRequest>(filePath);
                        // do something with cookers
                        break;
                    case "dish_cards":
                        dishCards = JsonSerializer.ReadJsonFile<DishCardRequest>(filePath);
                        // do something with dishCards
                        break;
                    case "equipment":
                        equipments = JsonSerializer.ReadJsonFile<EquipmentRequest>(filePath);
                        // do something with equipments
                        break;
                    case "equipment_type":
                        equipmentTypes = JsonSerializer.ReadJsonFile<EquipmentTypeRequest>(filePath);
                        // do something with equipmentTypes
                        break;
                    case "menu_dishes":
                        menuDishes = JsonSerializer.ReadJsonFile<MenuDishRequest>(filePath);
                        // do something with menuDishes
                        break;
                    case "operation_types":
                        operationTypes = JsonSerializer.ReadJsonFile<OperationTypeRequest>(filePath);
                        // do something with operationTypes
                        break;
                    case "product_types":
                        productTypes = JsonSerializer.ReadJsonFile<ProductTypeRequest>(filePath);
                        // do something with productTypes
                        break;
                    case "products":
                        products = JsonSerializer.ReadJsonFile<ProductRequest>(filePath);
                        // do something with products
                        break;
                    case "visitors_orders":
                        visitorsOrders = JsonSerializer.ReadJsonFile<VisitorOrderRequest>(filePath);
                        // do something with visitorsOrders
                        break;
                    default:
                        Console.WriteLine("!!! no class model found !!!");
                        // handle unknown file names
                        break;
                }
            }

            if (dishCards.ToList() != null)
            {
                var updatedDishCards = new List<DishCardRequest>();
                foreach (var card in dishCards)
                {
                    var updatedOperations = new List<OperationRequest>();
                    foreach (var op in card.Operations)
                    {
                        var updatedProducts = new List<ProductRequest>();
                        foreach (var product in op.Products)
                        {
                            foreach (var productType in products)
                            {
                                if (product.ProductTypeId == productType.ProductTypeId)
                                {
                                    var updatedProduct = new ProductRequest
                                    {
                                        Id = productType.Id,
                                        Name = productType.Name,
                                        Company = productType.Company,
                                        Quantity = productType.Quantity,
                                        Unit = productType.Unit,
                                        Cost = productType.Cost,
                                        ProductTypeId = productType.ProductTypeId,
                                        Delivered = productType.Delivered,
                                        ValidUntil = productType.ValidUntil
                                    };
                                    updatedProducts.Add(updatedProduct);
                                }
                            }
                        }
                        var updatedOp = op with { Products = updatedProducts};
                        updatedOperations.Add(updatedOp);
                    }
                    var updatedCard = card with { Operations = updatedOperations };
                    updatedDishCards.Add(updatedCard);
                }
                dishCards = updatedDishCards;
            }
            
            JsonSerializer.PrintList(dishCards);

            // start

            SuperVisor visor = new SuperVisor().
                BuildCookers(cookers).
                BuildDishCards(dishCards).
                BuildEquipments(equipmentTypes, equipments).
                BuildOperations(operationTypes).
                BuildProducts(productTypes, products).
                BuildMenuDishes(menuDishes).
                BuildVisitorsOrders(visitorsOrders);

            // action
            visor.Action();


            var p = new ProcessResponce
            {
                Id = 12,
                OrderDishId = 625,
                Started = default,
                Ended = default,
                IsActive = false,
                Operations = new List<ProcessOperationResponce>
                {
                    new()
                    {
                        Id = 1
                    },
                    new()
                    {
                        Id = 17
                    }
                }
            };

            var processList = new List<ProcessResponce>();
            processList.Add(p);
            
            JsonSerializer.WriteJsonFile(Path.Combine(Path.Combine(path, FileSettings.folderJsonOutput), "process_log.json"), processList, "process_log");
        }

    }
}

/*
 * 
*/