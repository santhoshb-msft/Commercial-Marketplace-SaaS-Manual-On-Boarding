using System.Threading;
using System.Threading.Tasks;

namespace CommandCenter.Webhook
{
    public interface IWebhookProcessor
    {
        Task ProcessWebhookNotificationAsync(WebhookPayload details, CancellationToken cancellationToken = default);
    }
}
