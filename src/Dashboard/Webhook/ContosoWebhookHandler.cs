using System;
using System.Threading.Tasks;
using Microsoft.Marketplace.Models;

namespace CommandCenter.Webhook
{
    public class ContosoWebhookHandler : IWebhookHandler
    {
        private readonly IMarketplaceNotificationHandler notificationHelper;

        public ContosoWebhookHandler(IMarketplaceNotificationHandler notificationHelper)
        {
            this.notificationHelper = notificationHelper;
        }

        public async Task ChangePlanAsync(WebhookPayload payload)
        {
            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                await notificationHelper.NotifyChangePlanAsync(NotificationModel.FromWebhookPayload(payload));
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
                await notificationHelper.ProcessChangeQuantityAsync(
                    NotificationModel.FromWebhookPayload(payload));
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await notificationHelper.ProcessOperationFailOrConflictAsync(
                    NotificationModel.FromWebhookPayload(payload));
            }
        }

        public async Task ReinstatedAsync(WebhookPayload payload)
        {
            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                await notificationHelper.ProcessReinstatedAsync(NotificationModel.FromWebhookPayload(payload));
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await notificationHelper.ProcessOperationFailOrConflictAsync(
                    NotificationModel.FromWebhookPayload(payload));
            }
        }

        public Task SubscribedAsync(WebhookPayload payload)
        {
            throw new NotImplementedException();
        }

        public async Task SuspendedAsync(WebhookPayload payload)
        {
            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                await notificationHelper.ProcessSuspendedAsync(NotificationModel.FromWebhookPayload(payload));
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await notificationHelper.ProcessOperationFailOrConflictAsync(
                    NotificationModel.FromWebhookPayload(payload));
            }
        }

        public async Task UnsubscribedAsync(WebhookPayload payload)
        {
            if (payload.Status == OperationStatusEnum.Succeeded)
            {
                await notificationHelper.ProcessUnsubscribedAsync(
                    NotificationModel.FromWebhookPayload(payload));
            }
            else if (payload.Status == OperationStatusEnum.Conflict || payload.Status == OperationStatusEnum.Failed)
            {
                await notificationHelper.ProcessOperationFailOrConflictAsync(
                    NotificationModel.FromWebhookPayload(payload));
            }
        }
    }
}