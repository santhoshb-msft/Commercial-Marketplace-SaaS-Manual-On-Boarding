// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Webhook
{
    using System;
    using System.Threading.Tasks;
    using CommandCenter.Marketplace;
    using Microsoft.Marketplace.SaaS;
    using Microsoft.Marketplace.SaaS.Models;

    /// <summary>
    /// Webhook handler.
    /// </summary>
    public class ContosoWebhookHandler : IWebhookHandler
    {
        private readonly IMarketplaceSaaSClient marketplaceClient;
        private readonly IMarketplaceNotificationHandler notificationHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContosoWebhookHandler"/> class.
        /// </summary>
        /// <param name="notificationHelper">Custom handler for notifications.</param>
        /// <param name="marketplaceClient">Marketplace client.</param>
        public ContosoWebhookHandler(
            IMarketplaceNotificationHandler notificationHelper,
            IMarketplaceSaaSClient marketplaceClient)
        {
            this.notificationHelper = notificationHelper;
            this.marketplaceClient = marketplaceClient;
        }

        /// <inheritdoc/>
        public async Task ChangePlanAsync(WebhookPayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                await this.marketplaceClient.Operations.UpdateOperationStatusAsync(
                        payload.SubscriptionId,
                        payload.OperationId,
                        new UpdateOperation { PlanId = payload.PlanId, Status = UpdateOperationStatusEnum.Success }).ConfigureAwait(false);
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await this.notificationHelper.ProcessOperationFailOrConflictAsync(payload).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task ChangeQuantityAsync(WebhookPayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                await this.marketplaceClient.Operations.UpdateOperationStatusAsync(
                        payload.SubscriptionId,
                        payload.OperationId,
                        new UpdateOperation { Quantity = payload.Quantity, Status = UpdateOperationStatusEnum.Success }).ConfigureAwait(false);
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await this.notificationHelper.ProcessOperationFailOrConflictAsync(payload).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task ReinstatedAsync(WebhookPayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                await this.notificationHelper.NotifyReinstatedAsync(payload).ConfigureAwait(false);
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await this.notificationHelper.ProcessOperationFailOrConflictAsync(payload).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task SuspendedAsync(WebhookPayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                await this.notificationHelper.NotifySuspendedAsync(payload)
                    .ConfigureAwait(false);
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await this.notificationHelper.ProcessOperationFailOrConflictAsync(payload).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task UnsubscribedAsync(WebhookPayload payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                await this.notificationHelper.NotifyUnsubscribedAsync(payload).ConfigureAwait(false);
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await this.notificationHelper.ProcessOperationFailOrConflictAsync(payload).ConfigureAwait(false);
            }
        }
    }
}
