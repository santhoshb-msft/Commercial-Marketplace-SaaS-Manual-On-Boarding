
using System.Threading.Tasks;

namespace Dashboard.Webhook
{
    public interface IWebhookHandler
    {
        Task ChangePlanAsync(WebhookPayload payload);

        Task ChangeQuantityAsync(WebhookPayload payload);

        Task ReinstatedAsync(WebhookPayload payload);

        Task SuspendedAsync(WebhookPayload payload);

        Task UnsubscribedAsync(WebhookPayload payload);
    }
}
