namespace MessageBus.RabbitMQ
{
    public class QueueConfiguration
    {
        public QueueConfiguration(string name, bool exclusive, string routingKey)
        {
            Name = name;
            Exclusive = exclusive;
            RoutingKey = routingKey;
        }

        public string Name { get; }
        public bool Exclusive { get; }
        public string RoutingKey { get; }

        public QueueConfiguration DeadLetter { get; set; }
    }
}
