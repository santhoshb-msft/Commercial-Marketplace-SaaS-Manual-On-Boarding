// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Models
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Marketplace.SaaS.Models;

    /// <summary>
    /// Update subscription view model.
    /// </summary>
    public class UpdateSubscriptionViewModel
    {
        /// <summary>
        /// Gets or sets available plans.
        /// </summary>
        public IList<Plan> AvailablePlans { get; set; }

        /// <summary>
        /// Gets or sets the current plan.
        /// </summary>
        public string CurrentPlan { get; set; }

        /// <summary>
        /// Gets or sets new plan.
        /// </summary>
        public string NewPlan { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether there are pending operations..
        /// </summary>
        public bool PendingOperations { get; set; }

        /// <summary>
        /// Gets or sets subscription  ID.
        /// </summary>
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets subscription name.
        /// </summary>
        public string SubscriptionName { get; set; }
    }
}