// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Utilities
{
    using System;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// A Uri builder in fluent style.
    /// </summary>
    public class FluentUriBuilder
    {
        private readonly UriBuilder uriBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentUriBuilder"/> class.
        /// </summary>
        /// <param name="uriBuilder">UriBuilder.</param>
        public FluentUriBuilder(UriBuilder uriBuilder)
        {
            this.uriBuilder = uriBuilder;
        }

        /// <summary>
        /// Gets built Uri.
        /// </summary>
        public Uri Uri => this.uriBuilder.Uri;

        /// <summary>
        /// Uri builder helper in fluent style.
        /// </summary>
        /// <param name="uri">Starting URI.</param>
        /// <returns>FluentUriBuilder.</returns>
        public static FluentUriBuilder Start(Uri uri)
        {
            return new FluentUriBuilder(new UriBuilder(uri));
        }

        /// <summary>
        /// Uri builder helper in fluent style.
        /// </summary>
        /// <param name="uriString">Starting URI.</param>
        /// <returns>FluentUriBuilder.</returns>
        public static FluentUriBuilder Start(string uriString)
        {
            if (string.IsNullOrEmpty(uriString))
            {
                throw new ArgumentNullException(nameof(uriString));
            }

            return new FluentUriBuilder(new UriBuilder(uriString.TrimEnd('/')));
        }

        /// <summary>
        /// Add a path to the URI.
        /// </summary>
        /// <param name="path">Path to be added.</param>
        /// <returns>FluentUriBUilder.</returns>
        public FluentUriBuilder AddPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            _ = path.Trim('/');
            this.uriBuilder.Path += this.uriBuilder.Path == "/" ? $"{path}" : $"/{path}";

            return this;
        }

        /// <summary>
        /// Add a query to the URI.
        /// </summary>
        /// <param name="queryParameterName">Uri paramter name.</param>
        /// <param name="queryParameter">Uri parameter value.</param>
        /// <returns>FLuentUriBuilder.</returns>
        public FluentUriBuilder AddQuery(string queryParameterName, string queryParameter)
        {
            if (string.IsNullOrEmpty(queryParameterName))
            {
                throw new ArgumentNullException(nameof(queryParameterName));
            }

            if (string.IsNullOrEmpty(queryParameter))
            {
                throw new ArgumentNullException(nameof(queryParameter));
            }

            var charsToRemove = new[] { '&', '?', '=' };

            var cleanParameterName = queryParameterName.Trim(charsToRemove);
            var cleanParameter = queryParameter.Trim(charsToRemove);

            var currentQuery = HttpUtility.ParseQueryString(this.uriBuilder.Uri.Query);
            currentQuery.Add(queryParameterName, queryParameter);
            this.uriBuilder.Query = currentQuery.AllKeys.Select(k => $"{k}={currentQuery[k]}")
                .Aggregate((working, next) => $"{working}&{next}");

            return this;
        }
    }
}