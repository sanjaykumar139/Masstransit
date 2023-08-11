using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransit.Consumer
{
    public interface IConsumer<in TMessage> : IConsumer where TMessage : class
    {
        Task Consume(ConsumeContext<TMessage> context);
    }
    public interface IConsumer { }
}
