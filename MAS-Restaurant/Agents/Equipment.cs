using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS_Restaurant.Agents;
internal class Equipment : IAgent
{
    public int EquipmentTypeId { get; set; }
    private string? _name;
    public int Count { get; set; }
    private Dictionary<int, IAgent> _agents;
    private CancelationToken _token;

    public Equipment(int equipmentTypeId,
        string? name,
        int count,
        Dictionary<int, IAgent> agents,
        CancelationToken token)
    {
        EquipmentTypeId = equipmentTypeId;
        _name = name;
        Count = count;
        _agents = agents;
        _token = token;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        Console.WriteLine("Equipment start working");
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
