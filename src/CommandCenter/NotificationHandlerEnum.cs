// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter
{
    /// <summary>
    /// Enum for the active notificaiton handler.
    /// </summary>
    public enum NotificationHandlerEnum
    {
        /// <summary>
        /// Sending emails.
        /// </summary>
        EmailNotifications,

        /// <summary>
        /// Send queue messages.
        /// </summary>
        AzureQueueNotifications,
    }
}