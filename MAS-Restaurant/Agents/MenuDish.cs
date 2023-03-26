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
        Console.WriteLine("MenuDish start working");

        while (_token.Atcive)
        {
            if (messages.Count > 0)
            {
                var message = messages.Pop();

                var dishCard = _agents.Where(x => x.Value is DishCard).
                    Select(x => x.Value as DishCard).
                    Where(x => x.id == dishCardId).
                    First();


            }
        }
    }

    public void GetMessage(Message message)
    {
        messages.Push(message);
    }

    public void SendMessage(IAgent agent, Message message)
    {
        throw new NotImplementedException();
    }
}
