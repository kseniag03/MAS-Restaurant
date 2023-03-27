using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;
internal class Cooker : IAgent
{
    private int _id;
    private string? _name;
    public bool IsActive { get; set; }
    public bool Cooking { get; set; }

    private Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public Stack<Message> messages = new();
    private Log _log;

    public Cooker(
        int id,
        string? name,
        bool isActive,
        Dictionary<int, IAgent> agents,
        CancelationToken token,
        Log log)
    {
        _id = id;
        _name = name;
        IsActive = isActive;
        Cooking = false;
        _agents = agents;
        _token = token;
        _log = log;
    }

    public void Action()
    {
        while (_token.Atcive)
        {
            if (messages.Count > 0)
            {
                IsActive = false;

                _log.AddLog("Cooker start working");
                Console.WriteLine("Cooker start working");

                var message = messages.Pop();

                IsOrderReady(message.Text, message);

                IsActive = true;
            }
            else
            {
                Thread.Sleep(1000);
            }
        }
    }

    public void GetMessage(Message message)
    {
        messages.Push(message);
    }

    public void SendMessage(IAgent agent, Message message)
    {
        agent.GetMessage(message);
    }

    private void IsOrderReady(List<int> idTime, Message message)
    {
        // id -- % 2 == 0, time -- % 2 == 1
        // 1 -- ready dish, -1 -- not ready dish

        /*
         * id equip and operation time
         * check if equipment is busy or not
         * if busy, wait
         * else execute operation given time
         */

        _log.AddLog("Cook take order");
        Console.WriteLine("Cook take order");

        List<Equipment> equipments = new List<Equipment>();
        List<int> operationTimes = new List<int>();

        Equipment equipment = null;
        int operationTime = 0;

        for (int i = 0; i < idTime.Count; ++i)
        {
            if (i % 2 == 0)
            {
                int equipmentId = idTime[i];
                equipment = _agents.Where(x => x.Value is Equipment).
                    Select(x => x.Value as Equipment).
                    Where(x => x.EquipmentTypeId == equipmentId).
                    First();
            }
            else
            {
                operationTime = idTime[i];

                if (equipment != null && equipment.Count > 0)
                {
                    equipment.Count -= 1;
                    _log.AddLog("Cook cooking");
                    Console.WriteLine("Cook cooking");
                    Thread.Sleep(operationTime * 10);
                    _log.AddLog("Cook finish cooking");
                    Console.WriteLine("Cook finish cooking");
                    equipment.Count += 1;
                }
                else
                {
                    _log.AddLog("Cook wait");
                    Console.WriteLine("Cook wait");
                    // no equipment
                    // else
                    // not active equipment ?
                    Thread.Sleep(1000);
                }
            }

            // GetMessage(); from equip to define if it is active or not
            // if not active

            _log.AddLog($"Cook send message to {message.Sender}");
            Console.WriteLine($"Cook send message to {message.Sender}");

            SendMessage(
                message.Sender,
                new Message(
                    this,
                    equipment,
                    new List<int>() { 1 })
            ); // send to equipment to check by id property isActive ?


        }

        _log.AddLog("Cook done");
        Console.WriteLine("Cook done");
    }
}