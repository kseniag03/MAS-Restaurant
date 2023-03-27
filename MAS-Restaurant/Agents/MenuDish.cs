using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;
internal class MenuDish : IAgent
{
    public int Id { get; set; }
    public int DishCardId { get; set; }
    public bool IsActive { get; set; }
    private Dictionary<int, IAgent> _agents;
    private CancelationToken _token;
    private Log _log;

    public MenuDish(int id,
        int dishCardId,
        bool isActive,
        Dictionary<int, IAgent> agents,
        CancelationToken token,
        Log log)
    {
        Id = id;
        DishCardId = dishCardId;
        IsActive = isActive;
        _agents = agents;
        _token = token;
        _log = log;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        while (_token.Atcive)
        {
            if (messages.Count > 0)
            {
                var message = messages.Pop();

                _log.AddLog("MenuDish start working");
                Console.WriteLine("MenuDish start working");

                var dishCard = _agents.Where(x => x.Value is DishCard).
                    Select(x => x.Value as DishCard).
                    Where(x => x.Id == DishCardId).
                    FirstOrDefault();

                if (dishCard != null && message != null)
                {
                    SendMessage(dishCard, new Message(message.Sender, dishCard, new List<int>() { }));
                }
            } 
            else
            {
                Thread.Sleep(1000);
            }
        }
    }

    public void GetMessage(Message message)
    {
        _log.AddLog("MenuDish take message");
        Console.WriteLine("MenuDish take message");
        messages.Push(message);
    }

    public void SendMessage(IAgent agent, Message message)
    {
        _log.AddLog($"MenuDish send message to {agent}");
        Console.WriteLine($"MenuDish send message to {agent}");
        agent.GetMessage(message);
    }
}
