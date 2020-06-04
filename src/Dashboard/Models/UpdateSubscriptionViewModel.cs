using System.Collections.Generic;

namespace Dashboard.Models
{
    using Microsoft.Marketplace.Models;

    using System;

    public class UpdateSubscriptionViewModel
    {
        public IList<Plan> AvailablePlans { get; set; }

        public string CurrentPlan { get; set; }

        public string NewPlan { get; set; }

        public bool PendingOperations { get; set; }

        public Guid SubscriptionId { get; set; }

        public string SubscriptionName { get; set; }
    }
}