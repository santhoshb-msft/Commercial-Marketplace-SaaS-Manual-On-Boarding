// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Marketplace
{
    using System;

    /// <summary>
    /// Options for marketplace client.
    /// </summary>
    public class MarketplaceClientOptions
    {
        /// <summary>
        /// Gets marketplace client name.
        /// </summary>
        public const string MarketplaceClient = "MarketplaceClient";

        /// <summary>
        /// Gets or sets tenant ID the app registration is on.
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// Gets or sets client ID of the app registration.
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets client secret for the app registration.
        /// </summary>
        public string AppKey { get; set; }
    }
}