using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Requests;
using MAS_Restaurant.Utility;
using System.Security.Cryptography.X509Certificates;

namespace MAS_Restaurant.Agents;
internal class SuperVisor : IAgent
{
    private Dictionary<int, Product> _products;
    private Dictionary<int, Equipment> _equipment;
    private Dictionary<int, Operation> _operations;
    private Dictionary<int, VisitorOrder> _orders;
    private Dictionary<int, MenuDish> _menu;
    private Dictionary<int, Cooker> _cookers;
    private Dictionary<int, DishCard> _instructions;
    public Dictionary<int, IAgent> _agents;

    CancelationToken _token;

    private string _log;

    public Stack<Message> messages = new();

    public SuperVisor()
    {
        // Initialization.
        _token = new CancelationToken();
        _products = new();
        _equipment = new();
        _operations = new();
        _orders = new();
        _menu = new();
        _cookers = new();
        _log = "";
        _agents = new();
    }

    public void Action()
    {
        ActivateThreads();

        while (_token.Atcive)
        {
            _token.Atcive = false;
        }
        // throw new NotImplementedException();
    }

    public void GetMessage(Message message)
    {
        throw new NotImplementedException();
    }

    public void SendMessage(IAgent agent, Message message)
    {
        throw new NotImplementedException();
    }

    // Build.
    public SuperVisor BuildCookers(IEnumerable<CookerRequest> cookers)
    {
        _cookers = cookers.ToDictionary(
            x => x.Id,
            x => new Cooker(
                x.Id,
                x.Name,
                x.IsActive,
                _agents,
                _token));

        return this;
    }

    public SuperVisor BuildDishCards(IEnumerable<DishCardRequest> dishCards) 
    {
        _instructions = dishCards.ToDictionary(
            x => x.Id,
            x => new DishCard(
                x.Id,
                x.Name,
                x.Operations,
                _agents,
                _token));

        return this;
    }

    public SuperVisor BuildEquipments(
        IEnumerable<EquipmentTypeRequest> equipmentTypes,
        IEnumerable<EquipmentRequest> equipments)
    {
        _equipment = equipmentTypes.ToDictionary(
            x => x.Id,
            x => new Equipment(
                x.Id,
                x.Name,
                0,
                _agents,
                _token));

        foreach (var item in equipments)
        {
            if (item.IsActive)
            {
                _equipment[item.EquipmentTypeId].Count += 1;
            }
        }

        return this;
    }

    public SuperVisor BuildOperations(IEnumerable<OperationTypeRequest> operationTypes)
    {
        _operations = operationTypes.ToDictionary(
            x => x.Id,
            x => new Operation(
                x.Id,
                x.Name,
                _agents,
                _token)
            );

        return this;
    }

    public SuperVisor BuildProducts(
        IEnumerable<ProductTypeRequest> productTypes,
        IEnumerable<ProductRequest> products)
    {
        _products = productTypes.ToDictionary(
            x => x.Id,
            x => new Product(
                x.Id,
                x.Name,
                x.IsFood,
                0,
                _agents,
                _token));

        foreach (var item in products)
        {
            _products[item.ProductTypeId].Count += 1;
        }

        return this;
    }

    public SuperVisor BuildMenuDishes(IEnumerable<MenuDishRequest> menu)
    {
        _menu = menu.ToDictionary(
            x => x.Id,
            x => new MenuDish(
                x.Id,
                x.DishCardId,
                x.IsActive,
                _agents,
                _token));

        return this;
    }

    public SuperVisor BuildVisitorsOrders(IEnumerable<VisitorOrderRequest> visitorsOrders)
    {
        int i = 0;

        _orders = visitorsOrders.ToDictionary(
            x => i,
            x => new VisitorOrder(
                i++,
                x.Name,
                x.Started,
                x.Ended,
                x.Total,
                x.Dishes,
                _agents,
                _token));

        return this;
    }

    public void ActivateThreads()
    {
        int index = 0;
        foreach (var item in _orders)
        {
            _agents.Add(index++, item.Value);
        }
        foreach (var item in _equipment)
        {
            _agents.Add(index++, item.Value);
        }
        foreach (var item in _instructions)
        {
            _agents.Add(index++, item.Value);
        }
        foreach (var item in _menu)
        {
            _agents.Add(index++, item.Value);
        }
        foreach (var item in _operations)
        {
            _agents.Add(index++, item.Value);
        }
        foreach (var item in _products)
        {
            _agents.Add(index++, item.Value);
        }
        foreach (var item in _cookers)
        {
            _agents.Add(index++, item.Value);
        }
        foreach (var item in _orders)
        {
            Thread thread = new Thread(item.Value.Action);
            thread.Start();
        }
        foreach (var item in _equipment)
        {
            Thread thread = new Thread(item.Value.Action);
            thread.Start();
        }
        foreach (var item in _instructions)
        {
            Thread thread = new Thread(item.Value.Action);
            thread.Start();
        }
        foreach (var item in _menu)
        {
            Thread thread = new Thread(item.Value.Action);
            thread.Start();
        }
        foreach (var item in _operations)
        {
            Thread thread = new Thread(item.Value.Action);
            thread.Start();
        }
        foreach (var item in _products)
        {
            Thread thread = new Thread(item.Value.Action);
            thread.Start();
        }
        foreach (var item in _cookers)
        {
            Thread thread = new Thread(item.Value.Action);
            thread.Start();
        }
    }
}