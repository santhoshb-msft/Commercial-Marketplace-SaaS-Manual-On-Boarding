// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Webhook
{
    using System;

    /// <summary>
    /// Options for webhook.
    /// </summary>
    public class ContosoWebhookHandlerOptions
    {
        /// <summary>
        /// Gets or sets base URL.
        /// </summary>
        public Uri BaseUrl { get; set; }
    }
}