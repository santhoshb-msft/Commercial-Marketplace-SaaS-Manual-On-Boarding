// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.OperationsStore
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for storing operations.
    /// </summary>
    public interface IOperationsStore
    {
        /// <summary>
        /// Get all subscription operations.
        /// </summary>
        /// <param name="subscriptionId">Subscription.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of operation records.</returns>
        Task<IEnumerable<OperationRecord>> GetAllSubscriptionRecordsAsync(Guid subscriptionId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Record operation.
        /// </summary>
        /// <param name="subscriptionId">Related subscription.</param>
        /// <param name="operationId">Operation ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task RecordAsync(
            Guid subscriptionId,
            Guid operationId,
            CancellationToken cancellationToken = default);
    }
}