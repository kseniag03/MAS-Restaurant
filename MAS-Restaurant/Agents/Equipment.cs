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
    public int EquipmentTypeId;
    public string? Name;
    public int Count;
    Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public Equipment(int equipmentTypeId, string? name, int count, Dictionary<int, IAgent> agents, CancelationToken token)
    {
        EquipmentTypeId = equipmentTypeId;
        Name = name;
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
        throw new NotImplementedException();
    }

    public void SendMessage(IAgent agent, Message message)
    {
        throw new NotImplementedException();
    }
}
