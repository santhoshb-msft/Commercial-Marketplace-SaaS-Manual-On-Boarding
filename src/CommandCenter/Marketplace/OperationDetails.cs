using System;

namespace CommandCenter.Marketplace
{
    public class OperationDetails
    {
        public Guid OperationId { get; set; }

        public TimeSpan RetryInterval { get; set; }

        public Guid SubscriptionId { get; set; }
    }
}