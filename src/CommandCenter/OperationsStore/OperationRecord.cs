// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.OperationsStore
{
    using System;
    using Microsoft.Azure.Cosmos.Table;

    /// <summary>
    /// Operation record entity.
    /// </summary>
    public class OperationRecord : TableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationRecord"/> class.
        /// </summary>
        /// <param name="subscriptionId">Subscription ID as the partition key.</param>
        /// <param name="operationId">Operation id as the row key.</param>
        public OperationRecord(string subscriptionId, string operationId)
        {
            this.PartitionKey = subscriptionId;
            this.RowKey = operationId;
        }

        /// <summary>
        /// Gets or sets the operation ID.
        /// </summary>
        public Guid OperationId
        {
            get => Guid.Parse(this.RowKey);

            set => this.RowKey = value.ToString();
        }

        /// <summary>
        /// Gets or sets the subscription ID.
        /// </summary>
        public Guid SubscriptionId
        {
            get => Guid.Parse(this.PartitionKey);

            set => this.PartitionKey = value.ToString();
        }
    }
}