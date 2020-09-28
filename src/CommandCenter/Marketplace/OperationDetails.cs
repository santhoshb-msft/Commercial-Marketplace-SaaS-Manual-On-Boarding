// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Marketplace
{
    using System;

    /// <summary>
    /// Operation details.
    /// </summary>
    public class OperationDetails
    {
        /// <summary>
        ///  Gets or sets operation ID.
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// Gets or sets retry Interval.
        /// </summary>
        public TimeSpan RetryInterval { get; set; }

        /// <summary>
        /// Gets or sets subscription ID.
        /// </summary>
        public Guid SubscriptionId { get; set; }
    }
}