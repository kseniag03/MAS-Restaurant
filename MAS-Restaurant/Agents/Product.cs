using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;

internal class Product : IAgent
{
    public int Id { get; set; }
    public string? Name { get; set; }
    private bool _isFood;
    public int Count { get; set; }
    Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public Product(int id,
        string? name, bool isFood,
        int count,
        Dictionary<int, IAgent> agents,
        CancelationToken token)
    {
        Id = id;
        Name = name;
        _isFood = isFood;
        Count = count;
        _agents = agents;
        _token = token;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        Console.WriteLine("Product start working");
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