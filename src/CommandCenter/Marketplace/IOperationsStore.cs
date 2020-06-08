using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandCenter.Marketplace
{
    public interface IOperationsStore
    {
        Task<IEnumerable<OperationRecord>> GetAllSubscriptionRecordsAsync(Guid subscriptionId,
            CancellationToken cancellationToken = default);

        Task RecordAsync(
            Guid subscriptionId,
            Guid operationId,
            CancellationToken cancellationToken = default);
    }
}