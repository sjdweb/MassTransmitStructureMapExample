namespace MassTransitStructureMapExample
{
    using MassTransit;

    using MassTransitStructureMapExample.Consumers;
    using MassTransitStructureMapExample.Messages;

    using StructureMap;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            
            container.Configure(
                x =>
                {
                    x.For<TestService>().Use<TestService>();
                });

            SetupRegistry(container);
            //SetupContainer(container);

            var testService = container.GetInstance<TestService>();

            System.Console.ReadLine();
        }

        private static void SetupContainer(IContainer container)
        {
            container.Configure(
                c =>
                {
                    //For<MyMessageConsumer>().Use<MyMessageConsumer>();
                    c.ForConcreteType<MyMessageConsumer>();
                });

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

        private static void SetupRegistry(IContainer container)
        {
            container.Configure(x => x.AddRegistry(new MassTransitRegistry(container)));
        }
    }

    public class TestService
    {
        public TestService(IServiceBus serviceBus)
        {
            serviceBus.Publish(new MyMessage { Blah = "Yup" });
        }
    }
}
