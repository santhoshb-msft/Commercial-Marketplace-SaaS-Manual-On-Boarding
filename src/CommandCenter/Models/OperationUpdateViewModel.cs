<<<<<<< HEAD
<<<<<<< HEAD
﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
=======
﻿using CommandCenter.Marketplace;
>>>>>>> afebf60... Removed legacy SDK client, added generated SDK

namespace CommandCenter.Models
{
    using CommandCenter.Webhook;
=======
﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Models
{
    using CommandCenter.Marketplace;
>>>>>>> 7ba2462... StyleCop fixes.

    /// <summary>
    /// Operation update view model.
    /// </summary>
    public class OperationUpdateViewModel
    {
        /// <summary>
<<<<<<< HEAD
        ///  Gets or sets operation type.
=======
        /// Gets or sets operation type.
>>>>>>> 7ba2462... StyleCop fixes.
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
<<<<<<< HEAD
        /// Gets or sets webhook payload.
=======
        /// Gets or sets payload.
>>>>>>> 7ba2462... StyleCop fixes.
        /// </summary>
        public WebhookPayload Payload { get; set; }
    }
}