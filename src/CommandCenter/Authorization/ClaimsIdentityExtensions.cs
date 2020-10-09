// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Authorization
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>
    /// Extension class for extracting email from the claims.
    /// </summary>
    public static class ClaimsIdentityExtensions
    {
        /// <summary>
        /// Gets user's email from the claims.
        /// </summary>
        /// <param name="principal">Identity of the user.</param>
        /// <returns>User's email.</returns>
        public static string GetUserEmail(this IIdentity principal)
        {
            if (!(principal is ClaimsIdentity identity))
            {
                throw new ApplicationException("Not of ClaimsIdentity type");
            }

            return string.IsNullOrEmpty(identity.Name)
                ? identity.FindFirst("preferred_username")?.Value
                : identity.Name;
        }
    }
}