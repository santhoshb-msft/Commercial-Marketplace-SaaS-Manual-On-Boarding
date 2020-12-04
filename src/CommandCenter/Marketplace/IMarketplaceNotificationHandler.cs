// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Marketplace
{
    using System.Threading;
    using System.Threading.Tasks;
    using CommandCenter.Models;

    /// <summary>
    ///  Notification helper interface.
    /// </summary>
    public interface IMarketplaceNotificationHandler
    {
        /// <summary>
        /// Notify about change plan.
        /// </summary>
        /// <param name="payload">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task NotifyChangePlanAsync(WebhookPayload payload, CancellationToken cancellationToken = default);

        /// <summary>
        /// Notify about new subscription.
        /// </summary>
        /// <param name="provisionModel">provisionModel model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessNewSubscriptionAsyc(
            AzureSubscriptionProvisionModel provisionModel,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Notify about new plan change.
        /// </summary>
        /// <param name="provisionModel">provisionModel model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessChangePlanAsync(
            AzureSubscriptionProvisionModel provisionModel,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Notify about change quantity.
        /// </summary>
        /// <param name="payload">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task NotifyChangeQuantityAsync(
            WebhookPayload payload,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Notify about conflict.
        /// </summary>
        /// <param name="payload">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task ProcessOperationFailOrConflictAsync(
            WebhookPayload payload,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Notify about reinstate.
        /// </summary>
        /// <param name="payload">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task NotifyReinstatedAsync(WebhookPayload payload, CancellationToken cancellationToken = default);

        /// <summary>
        /// Notify about suspend.
        /// </summary>
        /// <param name="payload">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task NotifySuspendedAsync(WebhookPayload payload, CancellationToken cancellationToken = default);

        /// <summary>
        /// Notify about unsbscribe.
        /// </summary>
        /// <param name="payload">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Void.</returns>
        Task NotifyUnsubscribedAsync(
            WebhookPayload payload,
            CancellationToken cancellationToken = default);
    }
}