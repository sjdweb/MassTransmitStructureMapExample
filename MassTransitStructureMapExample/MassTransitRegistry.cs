namespace MassTransitStructureMapExample
{
    using MassTransit;

    using MassTransitStructureMapExample.Consumers;

    using StructureMap;
    using StructureMap.Configuration.DSL;

    class MassTransitRegistry : Registry
    {
        public MassTransitRegistry(IContainer container)
        {
            //For<MyMessageConsumer>().Use<MyMessageConsumer>();
            ForConcreteType<MyMessageConsumer>();

            var bus = ServiceBusFactory.New(
                sbc =>
                {
                    sbc.UseMsmq();
                    sbc.ReceiveFrom("msmq://localhost/queue_55");
                    sbc.Subscribe(s => s.LoadFrom(container));
                    //sbc.Subscribe(s => s.Consumer<MyMessageConsumer>());
                });

            container.Inject(bus);
        }
    }
}
