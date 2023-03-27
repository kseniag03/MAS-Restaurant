using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;
internal class MenuDish : IAgent
{
    public int id;
    public int dishCardId;
    public bool isActive;
    Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public MenuDish(int id, int dishCardId, bool isActive, Dictionary<int, IAgent> agents, CancelationToken token)
    {
        this.id = id;
        this.dishCardId = dishCardId;
        this.isActive = isActive;
        _agents = agents;
        _token = token;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        while (_token.Atcive)
        {
            if (messages.Count > 0)
            {
                var message = messages.Pop();

                Console.WriteLine("MenuDish start working");

                var dishCard = _agents.Where(x => x.Value is DishCard).
                    Select(x => x.Value as DishCard).
                    Where(x => x.id == dishCardId).
                    First();

                SendMessage(dishCard, new Message(message.Sender, dishCard, new List<int>() {}));
            } 
            else
            {
                Console.WriteLine(messages.Count);
                Thread.Sleep(1000);
            }
        }
    }

    public void GetMessage(Message message)
    {
        Console.WriteLine("Dish take message");
        messages.Push(message);
    }

    public void SendMessage(IAgent agent, Message message)
    {
        agent.GetMessage(message);
    }
}
