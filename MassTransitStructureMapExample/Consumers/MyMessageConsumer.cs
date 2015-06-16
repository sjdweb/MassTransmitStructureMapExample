namespace MassTransitStructureMapExample.Consumers
{

    using System;

    using MassTransit;

    using MassTransitStructureMapExample.Messages;

    class MyMessageConsumer : Consumes<MyMessage>.Context
    {
        public void Consume(IConsumeContext<MyMessage> message)
        {
            System.Console.WriteLine(string.Format("Hello. Blah: {0}", message.Message.Blah));
        }
    }
}
