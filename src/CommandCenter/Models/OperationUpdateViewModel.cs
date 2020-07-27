<<<<<<< HEAD
﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
=======
﻿using CommandCenter.Marketplace;
>>>>>>> afebf60... Removed legacy SDK client, added generated SDK

namespace CommandCenter.Models
{
    using CommandCenter.Webhook;

    /// <summary>
    /// Operation update view model.
    /// </summary>
    public class OperationUpdateViewModel
    {
        /// <summary>
        ///  Gets or sets operation type.
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Gets or sets webhook payload.
        /// </summary>
        public WebhookPayload Payload { get; set; }
    }
}