using System;
using System.Threading.Tasks;
using CommandCenter.Marketplace;
using Microsoft.Marketplace;
using Microsoft.Marketplace.Models;

namespace CommandCenter.Webhook
{
    public class ContosoWebhookHandler : IWebhookHandler
    {
        private readonly IMarketplaceClient marketplaceClient;
        private readonly IMarketplaceNotificationHandler notificationHelper;

        public ContosoWebhookHandler(IMarketplaceNotificationHandler notificationHelper,
            IMarketplaceClient marketplaceClient)
        {
            this.notificationHelper = notificationHelper;
            this.marketplaceClient = marketplaceClient;
        }

        public async Task ChangePlanAsync(WebhookPayload payload)
        {
            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                var response = await marketplaceClient.SubscriptionOperations.UpdateOperationStatusWithHttpMessagesAsync(
                    payload.SubscriptionId, payload.OperationId, null, null, payload.PlanId,
                    null, UpdateOperationStatusEnum.Success);

                // Change request is complete
                if (response.Response.IsSuccessStatusCode)
                {
                    await notificationHelper.NotifyChangePlanAsync(NotificationModel.FromWebhookPayload(payload)); 

                }
                
            }

            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await notificationHelper.ProcessOperationFailOrConflictAsync(
                    NotificationModel.FromWebhookPayload(payload));
            }
        }

        public async Task ChangeQuantityAsync(WebhookPayload payload)
        {
            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                var response = await marketplaceClient.SubscriptionOperations.UpdateOperationStatusWithHttpMessagesAsync(
                    payload.SubscriptionId, payload.OperationId, null, null, null,
                    payload.Quantity, UpdateOperationStatusEnum.Success);

                // Change request is complete
                if (response.Response.IsSuccessStatusCode)
                {
                    await notificationHelper.ProcessChangeQuantityAsync(
                        NotificationModel.FromWebhookPayload(payload));

                }
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
                await notificationHelper.ProcessOperationFailOrConflictAsync(
                    NotificationModel.FromWebhookPayload(payload));
        }

        public async Task ReinstatedAsync(WebhookPayload payload)
        {
            if (payload.Status == OperationStatusEnum.Succeeded)
                await notificationHelper.ProcessReinstatedAsync(NotificationModel.FromWebhookPayload(payload));
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
                await notificationHelper.ProcessOperationFailOrConflictAsync(
                    NotificationModel.FromWebhookPayload(payload));
        }

        public async Task SuspendedAsync(WebhookPayload payload)
        {
            if (payload.Status == OperationStatusEnum.Succeeded)
                await notificationHelper.ProcessSuspendedAsync(NotificationModel.FromWebhookPayload(payload));
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
                await notificationHelper.ProcessOperationFailOrConflictAsync(
                    NotificationModel.FromWebhookPayload(payload));
        }

        public async Task UnsubscribedAsync(WebhookPayload payload)
        {
            if (payload.Status == OperationStatusEnum.Succeeded)
                await notificationHelper.ProcessUnsubscribedAsync(
                    NotificationModel.FromWebhookPayload(payload));
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
                await notificationHelper.ProcessOperationFailOrConflictAsync(
                    NotificationModel.FromWebhookPayload(payload));
        }

        public Task SubscribedAsync(WebhookPayload payload)
        {
            throw new NotImplementedException();
        }
    }
}