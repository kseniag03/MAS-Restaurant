using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;

internal class Product : IAgent
{
    public int Id { get; set; }
    public string? Name { get; set; }
    private bool _isFood;
    public int Count { get; set; }
    private Dictionary<int, IAgent> _agents;
    private CancelationToken _token;
    private Log _log;

    public Product(int id,
        string? name, bool isFood,
        int count,
        Dictionary<int, IAgent> agents,
        CancelationToken token, Log log)
    {
        Id = id;
        Name = name;
        _isFood = isFood;
        Count = count;
        _agents = agents;
        _token = token;
        _log = log;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        _log.AddLog("Product start working");
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