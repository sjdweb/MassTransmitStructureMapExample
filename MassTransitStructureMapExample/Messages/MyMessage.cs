namespace MassTransitStructureMapExample.Messages
{
    using System;

    using MassTransit;
    using MassTransit.Context;

    class MyMessage : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; private set; }

        public string Blah { get; set; }
    }
}
