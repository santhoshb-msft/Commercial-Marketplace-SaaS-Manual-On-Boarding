namespace Dashboard.Marketplace
{
    using Microsoft.Marketplace.Models;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class MarketplaceSubscription
    {
        public string OfferId { get; set; }

        public string PlanId { get; set; }

        public int Quantity { get; set; }

        public SubscriptionStatusEnum State { get; set; }

        public Guid SubscriptionId { get; set; }

        [Display(Name = "Name")]
        public string SubscriptionName { get; set; }

        internal static MarketplaceSubscription From(Subscription subscription, SubscriptionStatusEnum newState)
        {
            return new MarketplaceSubscription
            {
                SubscriptionId = subscription.Id.Value,
                OfferId = subscription.OfferId,
                PlanId = subscription.PlanId,
                Quantity = subscription.Quantity.Value,
                SubscriptionName = subscription.Name,
                State = newState
            };
        }

        internal static MarketplaceSubscription From(Subscription subscription)
        {
            return new MarketplaceSubscription
            {
                SubscriptionId = subscription.Id.Value,
                OfferId = subscription.OfferId,
                PlanId = subscription.PlanId,
                Quantity = subscription.Quantity.Value,
                SubscriptionName = subscription.Name,
                State = subscription.SaasSubscriptionStatus.Value
            };
        }
    }
}