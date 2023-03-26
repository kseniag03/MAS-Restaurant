using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Requests;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;
internal class DishCard : IAgent
{
    public int id;
    public string? name;
    public List<OperationRequest>? operations;
    Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public DishCard(int id, string? name, List<OperationRequest>? operations, Dictionary<int, IAgent> agents, CancelationToken token)
    {
        this.id = id;
        this.name = name;
        this.operations = operations;
        _agents = agents;
        _token = token;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        Console.WriteLine("DishCard start working");

        while (_token.Atcive)
        {
            if (messages.Count > 0)
            {
            }
            else
            {
                Thread.Sleep(1000);
            }
        }
    }

    public void GetMessage(Message message)
    {
        Console.WriteLine("DishCard get message");
        messages.Push(message);
    }

    public void SendMessage(IAgent agent, Message message)
    {
        throw new NotImplementedException();
    }
}
