using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Requests;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;
internal class VisitorOrder : IAgent
{
    private int Id;
    private string? name;
    private DateTimeOffset started;
    private DateTimeOffset ended;
    private double total;
    private List<OrderDishRequest>? dishes;
    Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public VisitorOrder(
        int id,
        string? name,
        DateTimeOffset started,
        DateTimeOffset ended,
        double total,
        List<OrderDishRequest>? dishes,
        Dictionary<int, IAgent> agents,
        CancelationToken token)
    {
        Id = id;
        this.name = name;
        this.started = started;
        this.ended = ended;
        this.total = total;
        this.dishes = dishes;
        _agents = agents;
        _token = token;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        Console.WriteLine("Visitor start working"); 

        var menuDish = _agents.
            Where(x => x.Value is MenuDish).
            Select(x => x.Value as MenuDish).
            Where(x => x.id == dishes[0].MenuDishId).
            First();

        SendMessage(menuDish, new Message(
            this,
            menuDish,
            dishes.Select(x => x.MenuDishId).ToList()));

        while (messages.Count == 0)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Visitor waiting for order");
        }

        if (messages.Pop(). == 1)
        {

        }

        Console.WriteLine("Visitor find dishCard");
    }

    public void GetMessage(Message message)
    {
        messages.Push(message);
    }

    public void SendMessage(IAgent agent, Message message)
    {
        agent.GetMessage(message);
    }
}
