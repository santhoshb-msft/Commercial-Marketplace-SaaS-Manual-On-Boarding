// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Authorization
{
    using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Authorize admins only.
/// </summary>
    public class CommandCenterAdminRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCenterAdminRequirement"/> class.
        /// </summary>
        /// <param name="commandCenterAdmin">Admin that needs to be checked.</param>
        public CommandCenterAdminRequirement(string commandCenterAdmin)
        {
            this.AdminName = commandCenterAdmin;
        }

        /// <summary>
        /// Gets the name of the admin.
        /// </summary>
        public string AdminName { get; }
    }
}