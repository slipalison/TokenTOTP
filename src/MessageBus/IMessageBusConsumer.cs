using EasyNetQ;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBus
{
    public interface IMessageBusConsumer<out T> : IDisposable
        where T : class
    {
        void RegisterHandler(Func<T, MessageProperties, CancellationTokenSource, Task> onProcess);
        void RegisterHandler(Func<T, MessageProperties, CancellationTokenSource, Task> onProcess, Action<T, MessageProperties, Exception> onError);

        void RegisterHandler(Func<T, MessageProperties, CancellationTokenSource, Task> onProcess, Func<T, MessageProperties, Task> onCancel);

        void RegisterHandler(Func<T, MessageProperties, CancellationTokenSource, Task> onProcess, Func<T, MessageProperties, Task> onCancel, Action<T, MessageProperties, Exception> onError);
    }
}