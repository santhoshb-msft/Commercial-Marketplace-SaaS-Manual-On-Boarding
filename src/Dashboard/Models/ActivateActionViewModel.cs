using System;

namespace CommandCenter.Models
{
    public class ActivateActionViewModel
    {
        public string PlanId { get; set; }

        public Guid SubscriptionId { get; set; }
    }
}