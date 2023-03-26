using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;

internal class Product : IAgent
{
    public int Id;
    public string? Name;
    public bool IsFood;
    public int Count;
    Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public Product(int id, string? name, bool isFood, int count, Dictionary<int, IAgent> agents, CancelationToken token)
    {
        Id = id;
        Name = name;
        IsFood = isFood;
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
        throw new NotImplementedException();
    }

    public void SendMessage(IAgent agent, Message message)
    {
        throw new NotImplementedException();
    }
}