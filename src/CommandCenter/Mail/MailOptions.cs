// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Mail
{
    /// <summary>
    /// Email related options.
    /// </summary>
    public class MailOptions
    {
        /// <summary>
        ///  Gets or sets operations team email.
        /// </summary>
        public string OperationsTeamEmail { get; set; }

        /// <summary>
        /// Gets or sets SendGrid API key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets from email address.
        /// </summary>
        public string FromEmail { get; set; }
    }
}