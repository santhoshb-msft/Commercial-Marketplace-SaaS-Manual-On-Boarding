using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Marketplace;
using Newtonsoft.Json;

namespace CommandCenter.Webhook
{
    public class WebhookProcessor : IWebhookProcessor
    {
        private readonly ILogger<WebhookProcessor> logger;
        private readonly IMarketplaceClient marketplaceClient;
        private readonly IWebhookHandler webhookHandler;

        public WebhookProcessor(IMarketplaceClient marketplaceClient,
            IWebhookHandler webhookHandler,
            ILogger<WebhookProcessor> logger)
        {
            this.logger = logger;
            this.marketplaceClient = marketplaceClient;
            this.webhookHandler = webhookHandler;
        }

        public async Task ProcessWebhookNotificationAsync(WebhookPayload payload,
            CancellationToken cancellationToken = default)
        {
            // Always query the fulfillment API for the received Operation for security reasons. Webhook endpoint is not authenticated.
            var operationDetails = await marketplaceClient.SubscriptionOperations.GetOperationStatusAsync(
                payload.SubscriptionId,
                payload.OperationId,
                null,
                null,
                cancellationToken);

            if (operationDetails == null)
            {
                logger.LogError(
                    $"Operation query returned {JsonConvert.SerializeObject(operationDetails)} for subscription {payload.SubscriptionId} operation {payload.OperationId}");
                return;
            }

            switch (payload.Action)
            {
                case WebhookAction.Unsubscribe:
                    await webhookHandler.UnsubscribedAsync(payload);
                    break;

                case WebhookAction.ChangePlan:
                    await webhookHandler.ChangePlanAsync(payload);
                    break;

                case WebhookAction.ChangeQuantity:
                    await webhookHandler.ChangeQuantityAsync(payload);
                    break;

                case WebhookAction.Suspend:
                    await webhookHandler.SuspendedAsync(payload);
                    break;

                case WebhookAction.Reinstate:
                    await webhookHandler.ReinstatedAsync(payload);
                    break;

                case WebhookAction.Transfer:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}