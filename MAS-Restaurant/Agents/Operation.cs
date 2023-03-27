using MAS_Restaurant.Interfaces;
using MAS_Restaurant.Utility;

namespace MAS_Restaurant.Agents;
internal class Operation : IAgent
{
    public int Id { get; set; }
    private string? _name;
    private Dictionary<int, IAgent> _agents;
    private CancelationToken _token;

    public Operation(int id, string? name, Dictionary<int, IAgent> agents, CancelationToken token)
    {
        Id = id;
        _name = name;
        _agents = agents;
        _token = token;
    }

    public Stack<Message> messages = new();

    /*
     * check all products (store, product count property compare with operation count property)
     * -1 to order if not enough products (check each)
     * else to cooker transfer list equipment and operation time (odd even)
     * 
     * dishcard send message to operation
     * (where sender -- order, receiver -- operation, text: does not matter, list is empty)
     *
     * id id id, -1, id, time, id, time, id, time
     * before -- product, after -- equipment
     * 9 sugar points == 9 sugar_id_cnt
     *
     */

    public void Action()
    {
        while (_token.Atcive)
        {
            if (messages.Count > 0)
            {
                Console.WriteLine($"Operation {Id} start working");
                var message = messages.Pop();
                var list = message.Text;

                var operationsCount = list.
                    SkipWhile(x => x != -1).
                    Skip(1).
                    ToList();
                var productsWithRepeat = list.TakeWhile(x => x != -1).ToList();
                var productsCount = new Dictionary<int, int>();

                foreach (var id in productsWithRepeat)
                {
                    if (productsCount.ContainsKey(id))
                    {
                        ++productsCount[id];
                    }
                    else
                    {
                        productsCount[id] = 1;
                    }
                }

                bool flag = true;

                foreach (var product in productsCount)
                {
                    var storeProduct = _agents
                        .Where(x => x.Value is Product)
                        .Select(x => x.Value as Product)
                        .Where(x => x.Id == product.Key)
                        .FirstOrDefault();

                    if (storeProduct == null)
                    {
                        flag = false;
                    }
                    else if (storeProduct.Count < product.Value)
                    {
                        Console.WriteLine($"Not enough {storeProduct.Name}");
                        SendMessage(message.Sender, new Message(
                                this,
                                message.Sender,
                                new List<int>() { -1 }
                            )
                        );
                        flag = false;
                    }
                }

                if (flag)
                {
                    var cooker = _agents
                        .Where(x => x.Value is Cooker)
                        .Select(x => x.Value as Cooker)
                        .Where(x => x.IsActive)
                        .First();

                    SendMessage(cooker, new Message(
                            message.Sender,
                            cooker,
                            operationsCount
                        )
                    );
                }
            }
        }
    }

    public void GetMessage(Message message)
    {
        Console.WriteLine("Operation take message");
        messages.Push(message);
    }

    public void SendMessage(IAgent agent, Message message)
    {
        Console.WriteLine($"Operation send message to {agent}");
        agent.GetMessage(message);
    }
}
