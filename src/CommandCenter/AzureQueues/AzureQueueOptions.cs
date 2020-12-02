// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter
{
    /// <summary>
    ///  Options for Azure queues notification adapter.
    /// </summary>
    public class AzureQueueOptions
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string StorageConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the queue name.
        /// </summary>
        public string QueueName { get; set; }
    }
}