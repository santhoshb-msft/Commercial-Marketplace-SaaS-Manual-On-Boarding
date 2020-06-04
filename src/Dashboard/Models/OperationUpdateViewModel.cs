namespace Dashboard.Models
{
    using Dashboard.Webhook;

    public class OperationUpdateViewModel
    {
        public string OperationType { get; set; }

        public WebhookPayload Payload { get; set; }
    }
}