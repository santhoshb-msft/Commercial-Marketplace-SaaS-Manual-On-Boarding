// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Marketplace
{
    using System;
    using CommandCenter.Webhook;

    /// <summary>
    /// Notification DTO.
    /// </summary>
    public class NotificationModel
    {
        /// <summary>
        /// Gets or sets offer ID.
        /// </summary>
        public string OfferId { get; set; }

        /// <summary>
        /// Gets or sets operation ID.
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// Gets or sets operation type.
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Gets or sets plan ID.
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// Gets or sets publisher id.
        /// </summary>
        public string PublisherId { get; set; }

        /// <summary>
        /// Gets or sets quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets Subscription ID.
        /// </summary>
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Creates a notification model from webhook payload.
        /// </summary>
        /// <param name="payload">Webhook payload.</param>
        /// <returns>NotificationModel.</returns>
        public static NotificationModel FromWebhookPayload(WebhookPayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            return new NotificationModel
            {
                OfferId = payload.OfferId,
                OperationId = payload.OperationId,
                PlanId = payload.PlanId,
                PublisherId = payload.PublisherId,
                Quantity = payload.Quantity,
                SubscriptionId = payload.SubscriptionId,
            };
        }
    }
}