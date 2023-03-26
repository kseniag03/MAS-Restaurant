using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;
internal class Cooker : IAgent
{
    private int id;
    private string? name;
    private bool isActive;
    public bool Cooking;
    private Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public Stack<Message> messages = new();

    public Cooker(
        int id,
        string? name,
        bool isActive,
        Dictionary<int, IAgent> agents,
        CancelationToken token)
    {
        this.id = id;
        this.name = name;
        this.isActive = isActive;
        Cooking = false;
        _agents = agents;
    }

    public void Action()
    {
        Console.WriteLine("Cooker start working");

        while (_token.Atcive)
        {
            if (messages.Count > 0 )
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
        throw new NotImplementedException();
    }

    public void SendMessage(IAgent agent, Message message)
    {
        throw new NotImplementedException();
    }
}