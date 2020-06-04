namespace Dashboard.Models
{
    using System.Collections.Generic;

    using Microsoft.Marketplace.Models;

    public class OperationsViewModel
    {
        public List<Operation> Operations { get; set; }

        public string SubscriptionName { get; set; }
    }
}