namespace MessageBus.Options
{
    public class MessageBusOptions
    {
        public string DefaultSerializer { get; set; }

        public MessageBusOptions()
        {
            DefaultSerializer = "application/json";
        }
    }
}
