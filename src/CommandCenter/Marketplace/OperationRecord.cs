using System;
using Microsoft.Azure.Cosmos.Table;

namespace CommandCenter.Marketplace
{
    public class OperationRecord : TableEntity
    {
        public OperationRecord(string subscriptionId, string operationId)
        {
            PartitionKey = subscriptionId;
            RowKey = operationId;
        }

        public Guid OperationId
        {
            get => Guid.Parse(RowKey);

            set => RowKey = value.ToString();
        }

        public Guid SubscriptionId
        {
            get => Guid.Parse(PartitionKey);

            set => PartitionKey = value.ToString();
        }
    }
}