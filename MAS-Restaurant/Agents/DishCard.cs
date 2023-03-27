using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Requests;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;
internal class DishCard : IAgent
{
    public int id;
    public string? name;
    public List<OperationRequest>? _operations;
    private int _equipmetType;
    Dictionary<int, IAgent> _agents;
    CancelationToken _token;

    public DishCard(int id, string? name, List<OperationRequest>? operations, int equipmentType, Dictionary<int, IAgent> agents, CancelationToken token)
    {
        this.id = id;
        this.name = name;
        _operations = operations;
        _equipmetType = equipmentType;
        _agents = agents;
        _token = token;
    }

    public Stack<Message> messages = new();

    public void Action()
    {
        Console.WriteLine("DishCard start working");

        while (_token.Atcive)
        {
            if (messages.Count > 0)
            {
                Console.WriteLine("Dish");

                var message = messages.Pop();

                SendMessage(message.Sender, new Message(
                    this,
                    message.Sender,
                    new List<int>() { _operations.Count }));

                foreach (var item in _operations)
                {
                    var operation = _agents.
                        Where(x => x.Value is Operation).
                        Select(x => x.Value as Operation).
                        Where(x => x.id == item.OperationTypeId).
                        First();

                    List<int> request = new();

                    foreach (var product in item.Products)
                    {
                        request.Add(product.ProductTypeId);
                    }

                    request.Add(-1);
                    request.Add(_equipmetType);
                    request.Add((int)item.Time);

                    string res = "";
                    foreach (var x in request)
                    {
                        res += x + " ";
                    }
                    Console.WriteLine(res);

                    SendMessage(operation, new Message(
                        message.Sender,
                        operation,
                        request
                        ));
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
        Console.WriteLine("DishCard get message");
        messages.Push(message);
    }

    public void SendMessage(IAgent agent, Message message)
    {
        agent.GetMessage(message);
    }
}
