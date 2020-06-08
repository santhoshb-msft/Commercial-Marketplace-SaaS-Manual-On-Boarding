using System;
using System.Collections.Generic;
using CommandCenter.Marketplace;
using Microsoft.Marketplace.Models;

namespace CommandCenter.Models
{
    public class SubscriptionViewModel : MarketplaceSubscription
    {
        public IEnumerable<ActionsEnum> NextActions
        {
            get
            {
                if (State == SubscriptionStatusEnum.PendingFulfillmentStart)
                {
                    return new List<ActionsEnum> { ActionsEnum.Activate };
                }

                if (State == SubscriptionStatusEnum.Subscribed) {
                    return new List<ActionsEnum> { ActionsEnum.Update, ActionsEnum.Unsubscribe };
                }

                return new List<ActionsEnum>();
            }
        }

        public bool PendingOperations { get; set; }

        public static SubscriptionViewModel FromSubscription(Subscription marketplaceSubscription)
        {
            return new SubscriptionViewModel
                       {
                           PlanId = marketplaceSubscription.PlanId,
                           Quantity = marketplaceSubscription.Quantity ?? 0,
                           SubscriptionId = marketplaceSubscription.Id ?? Guid.Empty,
                           OfferId = marketplaceSubscription.OfferId,
                           State = marketplaceSubscription.SaasSubscriptionStatus ?? SubscriptionStatusEnum.NotStarted,
                           SubscriptionName = marketplaceSubscription.Name,
                           PurchaserEmail = marketplaceSubscription.Purchaser.EmailId,
                           PurchaserTenantId = marketplaceSubscription.Purchaser.TenantId ?? Guid.Empty
                       };
        }

        public Guid PurchaserTenantId { get; set; }

        public string PurchaserEmail { get; set; }
        public bool ExistingOperations { get; internal set; }
        public int OperationCount { get; internal set; }
    }
}