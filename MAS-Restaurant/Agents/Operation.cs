using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;
internal class Operation : IAgent
{
    private int id;
    private string? name;
    Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public Operation(int id, string? name, Dictionary<int, IAgent> agents, CancelationToken token)
    {
        this.id = id;
        this.name = name;
        _agents = agents;
        _token = token;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        Console.WriteLine("Operation start working");
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
