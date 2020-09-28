// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Webhook
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Microsoft.Marketplace.SaaS;
    using Newtonsoft.Json;

    /// <summary>
    /// Webhook processor.
    /// </summary>
    public class WebhookProcessor : IWebhookProcessor
    {
        private readonly ILogger<WebhookProcessor> logger;
        private readonly IMarketplaceSaaSClient marketplaceClient;
        private readonly IWebhookHandler webhookHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookProcessor"/> class.
        /// </summary>
        /// <param name="marketplaceClient">Marketplace API client.</param>
        /// <param name="webhookHandler">Webhook handler.</param>
        /// <param name="logger">Logger.</param>
        public WebhookProcessor(
            IMarketplaceSaaSClient marketplaceClient,
            IWebhookHandler webhookHandler,
            ILogger<WebhookProcessor> logger)
        {
            this.logger = logger;
            this.marketplaceClient = marketplaceClient;
            this.webhookHandler = webhookHandler;
        }

        /// <inheritdoc/>
        public async Task ProcessWebhookNotificationAsync(
            WebhookPayload payload,
            CancellationToken cancellationToken = default)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            // Always query the fulfillment API for the received Operation for security reasons. Webhook endpoint is not authenticated.
            var operationDetails = await this.marketplaceClient.SubscriptionOperations.GetOperationStatusAsync(
                payload.SubscriptionId,
                payload.OperationId,
                null,
                null,
                cancellationToken).ConfigureAwait(false);

            if (operationDetails == null)
            {
                this.logger.LogError(
                    $"Operation query returned {JsonConvert.SerializeObject(operationDetails)} for subscription {payload.SubscriptionId} operation {payload.OperationId}");
                return;
            }

            switch (payload.Action)
            {
                case WebhookAction.Unsubscribe:
                    await this.webhookHandler.UnsubscribedAsync(payload).ConfigureAwait(false);
                    break;

                case WebhookAction.ChangePlan:
                    await this.webhookHandler.ChangePlanAsync(payload).ConfigureAwait(false);
                    break;

                case WebhookAction.ChangeQuantity:
                    await this.webhookHandler.ChangeQuantityAsync(payload).ConfigureAwait(false);
                    break;

                case WebhookAction.Suspend:
                    await this.webhookHandler.SuspendedAsync(payload).ConfigureAwait(false);
                    break;

                case WebhookAction.Reinstate:
                    await this.webhookHandler.ReinstatedAsync(payload).ConfigureAwait(false);
                    break;

                case WebhookAction.Transfer:
                    break;
                default:
                    throw new InvalidEnumArgumentException("payload.Action");
            }
        }
    }
}