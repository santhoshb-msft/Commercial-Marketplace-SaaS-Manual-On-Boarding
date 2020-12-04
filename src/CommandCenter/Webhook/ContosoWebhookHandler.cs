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
                var response = await this.marketplaceClient.SubscriptionOperations.UpdateOperationStatusWithHttpMessagesAsync(
                    payload.SubscriptionId,
                    payload.OperationId,
                    null,
                    null,
                    payload.PlanId,
                    null,
                    UpdateOperationStatusEnum.Success).ConfigureAwait(false);

                // Change request is complete
                if (response.Response.IsSuccessStatusCode)
                {
                    await this.notificationHelper.NotifyChangePlanAsync(payload).ConfigureAwait(false);
                }
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
                Microsoft.Rest.Azure.AzureOperationResponse response = await this.marketplaceClient.SubscriptionOperations.UpdateOperationStatusWithHttpMessagesAsync(
                    payload.SubscriptionId,
                    payload.OperationId,
                    null,
                    null,
                    null,
                    payload.Quantity,
                    UpdateOperationStatusEnum.Success).ConfigureAwait(false);

                // Change request is complete
                if (response.Response.IsSuccessStatusCode)
                {
                    await this.notificationHelper.NotifyChangeQuantityAsync(payload).ConfigureAwait(false);
                }
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