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

    public Cooker(
        int id,
        string? name,
        bool isActive,
        Dictionary<int, IAgent> agents,
        CancelationToken token)
    {
        _id = id;
        _name = name;
        IsActive = isActive;
        Cooking = false;
        _agents = agents;
        _token = token;
    }

    public void Action()
    {
        while (_token.Atcive)
        {
            if (messages.Count > 0)
            {
                IsActive = false;

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
                    Console.WriteLine("Cook cooking");
                    Thread.Sleep(operationTime * 10);
                    Console.WriteLine("Cook finish cooking");
                    equipment.Count += 1;
                }
                else
                {
                    Console.WriteLine("Cook wait");
                    // no equipment
                    // else
                    // not active equipment ?
                    Thread.Sleep(1000);
                }
            }

            // GetMessage(); from equip to define if it is active or not
            // if not active

            Console.WriteLine($"Cook send message to {message.Sender}");

            SendMessage(
                message.Sender,
                new Message(
                    this,
                    equipment,
                    new List<int>() { 1 })
            ); // send to equipment to check by id property isActive ?


        }

        Console.WriteLine("Cook done");
    }
}