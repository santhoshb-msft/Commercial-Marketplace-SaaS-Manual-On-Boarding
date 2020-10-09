// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Marketplace
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Marketplace.SaaS.Models;

    /// <summary>
    /// DTO for marketplace subscription.
    /// </summary>
    public class MarketplaceSubscription
    {
        /// <summary>
        /// Gets or sets offer ID.
        /// </summary>
        public string OfferId { get; set; }

        /// <summary>
        /// Gets or sets plan ID.
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// Gets or sets quantitiy.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets state.
        /// </summary>
        public SubscriptionStatusEnum State { get; set; }

        /// <summary>
        /// Gets or sets the subscription ID.
        /// </summary>
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the subscription name.
        /// </summary>
        [Display(Name = "Name")]
        public string SubscriptionName { get; set; }

        /// <summary>
        /// Creates a marketplace subscription object, given subscription and status.
        /// </summary>
        /// <param name="subscription">Subscription.</param>
        /// <param name="newState">New state.</param>
        /// <returns>MarketplaceSubscription.</returns>
        internal static MarketplaceSubscription From(Subscription subscription, SubscriptionStatusEnum newState) => new MarketplaceSubscription
        {
            SubscriptionId = subscription.Id.Value,
            OfferId = subscription.OfferId,
            PlanId = subscription.PlanId,
            Quantity = subscription.Quantity.Value,
            SubscriptionName = subscription.Name,
            State = newState,
        };

        /// <summary>
        /// Creates a marketplace subscription object, given subscription and status.
        /// </summary>
        /// <param name="subscription">Subscription.</param>
        /// <returns>MarketplaceSubscription.</returns>
        internal static MarketplaceSubscription From(Subscription subscription)
        {
            return new MarketplaceSubscription
            {
                SubscriptionId = subscription.Id.Value,
                OfferId = subscription.OfferId,
                PlanId = subscription.PlanId,
                Quantity = subscription.Quantity.Value,
                SubscriptionName = subscription.Name,
                State = subscription.SaasSubscriptionStatus.Value,
            };
        }
    }
}