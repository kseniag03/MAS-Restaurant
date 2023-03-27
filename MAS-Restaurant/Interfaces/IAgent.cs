using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Interfaces;
internal interface IAgent
{
    public void Action();

    public void GetMessage(Message message);

    public void SendMessage(IAgent agent, Message message);
}