namespace MessageBus.RabbitMQ
{
    public class ExchangeConfiguration
    {
        public ExchangeConfiguration(ExchangeTypes type, string name)
        {
            Type = type;
            Name = name;
        }

        public ExchangeTypes Type { get; }
        public string Name { get; }
    }
}
