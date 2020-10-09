// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Models
{
    using System;

    /// <summary>
    /// Activate action view model.
    /// </summary>
    public class ActivateActionViewModel
    {
        /// <summary>
        /// Gets or sets plan ID.
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// Gets or sets subscription ID.
        /// </summary>
        public Guid SubscriptionId { get; set; }
    }
}