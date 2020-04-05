using System.Diagnostics.CodeAnalysis;

namespace TokenTOTP.Shared.Configurations
{
    [ExcludeFromCodeCoverage]
    public class QueueConfiguration
    {
        public string Queue { get; set; }
        public string RountingKey { get; set; }

        public void Deconstruct(out string queue, out string rountingKey)
        {
            queue = Queue;
            rountingKey = RountingKey;
        }
    }
}