
using System.Threading;
using System.Threading.Tasks;

namespace Dashboard.Webhook
{
    public interface IWebhookProcessor
    {
        Task ProcessWebhookNotificationAsync(WebhookPayload details, CancellationToken cancellationToken = default);
    }
}
