// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Webhook
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///  Interface for webhook processor.
    /// </summary>
    public interface IWebhookProcessor
    {
        /// <summary>
        /// Process the notification.
        /// </summary>
        /// <param name="details">Webhook payload.</param>
        /// <param name="cancellationToken">cancellation token.</param>
        /// <returns>void.</returns>
        Task ProcessWebhookNotificationAsync(WebhookPayload details, CancellationToken cancellationToken = default);
    }
}