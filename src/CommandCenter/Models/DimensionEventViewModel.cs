// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using CommandCenter.DimensionUsageStore;

    /// <summary>
    /// Dimension view model.
    /// </summary>
    public class DimensionEventViewModel
    {
        /// <summary>
        /// Gets or sets the SubscriptionId.
        /// </summary>
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the SubscriptionName.
        /// </summary>
        public string SubscriptionName { get; set; }

        /// <summary>
        /// Gets or sets the OfferId.
        /// </summary>
        public string OfferId { get; set; }

        /// <summary>
        /// Gets or sets the PlanId.
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// Gets or sets the SubscriptionDimensions.
        /// </summary>
        public IEnumerable<string> SubscriptionDimensions { get; set; }

        /// <summary>
        /// Gets or sets the SelectedDimension.
        /// </summary>
        [Required]
        public string SelectedDimension { get; set; }

        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the EventTime.
        /// </summary>
        [Required]
        [DisplayName("Event Time In UTC")]
        public DateTime EventTime { get; set; }

        /// <summary>
        /// Gets or sets the PastUsageEvents.
        /// </summary>
        public IEnumerable<DimensionUsageRecord> PastUsageEvents { get; set; }
    }
}
