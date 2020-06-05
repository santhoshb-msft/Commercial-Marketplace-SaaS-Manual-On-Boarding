using CommandCenter.Mail;

namespace CommandCenter
{
    public class DashboardOptions
    {
        public string BaseUrl { get; set; }

        public string DashboardAdmin { get; set; }

        public MailOptions Mail { get; set; }

        public bool ShowUnsubscribed { get; set; }
    }
}