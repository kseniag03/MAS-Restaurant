using MAS_Restaurant.Interfaces;

namespace MAS_Restaurant.Utility;
internal class Message
{
    public IAgent Sender;
    public IAgent Receiver;
    public List<int> Text;

    public Message(IAgent sender, IAgent receiver, List<int> text)
    {
        Sender = sender;
        Receiver = receiver;
        Text = text;
    }
}