// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Marketplace
{
    using System.Threading;
    using System.Threading.Tasks;
    using CommandCenter.Models;

    /// <summary>
    /// Notification handler interface.
    /// </summary>
    public interface IMarketplaceNotificationHandler
    {
        /// <summary>
        /// Send email for plan change notification.
        /// </summary>
        /// <param name="notificationModel">Received notification.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task NotifyChangePlanAsync(NotificationModel notificationModel, CancellationToken cancellationToken = default);

        /// <summary>
        /// New subscription notification.
        /// </summary>
        /// <param name="provisionModel">Details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessActivateAsync(
            AzureSubscriptionProvisionModel provisionModel,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Send email for change plan notification.
        /// </summary>
        /// <param name="provisionModel">Details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessChangePlanAsync(
            AzureSubscriptionProvisionModel provisionModel,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Send email for quantity change.
        /// </summary>
        /// <param name="notificationModel">Details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessChangeQuantityAsync(
            NotificationModel notificationModel,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Handle fail of conflict operations.
        /// </summary>
        /// <param name="notificationModel">Received notification.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessOperationFailOrConflictAsync(
            NotificationModel notificationModel,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Send reinstated email.
        /// </summary>
        /// <param name="notificationModel">Received notification.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessReinstatedAsync(NotificationModel notificationModel, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send suspended email.
        /// </summary>
        /// <param name="notificationModel">Received notification.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessSuspendedAsync(NotificationModel notificationModel, CancellationToken cancellationToken = default);

        /// <summary>
        /// Send unsubscribed email.
        /// </summary>
        /// <param name="notificationModel">Received notification.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessUnsubscribedAsync(
            NotificationModel notificationModel,
            CancellationToken cancellationToken = default);
    }
}