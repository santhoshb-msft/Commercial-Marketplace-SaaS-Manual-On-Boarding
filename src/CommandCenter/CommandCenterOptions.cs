using CommandCenter.Mail;

namespace CommandCenter
{
    public class CommandCenterOptions
    {
        public string BaseUrl { get; set; }

        public string CommandCenterAdmin { get; set; }

        public MailOptions Mail { get; set; }

        public bool ShowUnsubscribed { get; set; }
    }
}