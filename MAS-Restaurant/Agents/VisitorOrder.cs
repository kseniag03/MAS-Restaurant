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
    public bool Done;
    private Dictionary<int, IAgent> _agents;
    private CancelationToken _token;

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
        Done = false;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        Console.WriteLine("Visitor start working"); 

        var menuDish = _agents.
            Where(x => x.Value is MenuDish).
            Select(x => x.Value as MenuDish).
            Where(x => x.Id == dishes[0].MenuDishId).
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

        if (messages.Where(x => x.Sender is DishCard).FirstOrDefault() != null)
        {
            var message = messages.Where(x => x.Sender is DishCard).First();

            int index = message.Text[0] + 1;

            while (index > 0)
            {
                if (messages.Count > 1)
                {
                    messages.Pop();
                    index -= 1;
                }
                else
                {
                    Console.WriteLine("Visitor waiting for order");
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine("Visitor take order");
            Done = true;
        }
        else
        {
            Thread.Sleep(1000);
            Console.WriteLine("Visitor waiting for order");
        }
    }

    public void GetMessage(Message message)
    {
        messages.Push(message);
    }

    public void SendMessage(IAgent agent, Message message)
    {
        Console.WriteLine($"Visitor send message to {agent}");
        agent.GetMessage(message);
    }
}
