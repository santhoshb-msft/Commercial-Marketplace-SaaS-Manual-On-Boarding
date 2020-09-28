// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Controllers
{
    using System;
    using System.Threading.Tasks;
    using CommandCenter.Webhook;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    /// <summary>
    /// Webhook endpoint.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [RequireHttps]

    // [AllowAnonymous]
    public class WebHookController : Controller
    {
        private readonly ILogger<WebHookController> logger;

        private readonly CommandCenterOptions options;

        private readonly IWebhookProcessor webhookProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebHookController"/> class.
        /// </summary>
        /// <param name="webhookProcessor">Webhook processor.</param>
        /// <param name="optionsMonitor">Command center options.</param>
        /// <param name="logger">Logger.</param>
        public WebHookController(
            IWebhookProcessor webhookProcessor,
            IOptionsMonitor<CommandCenterOptions> optionsMonitor,
            ILogger<WebHookController> logger)
        {
            if (optionsMonitor == null)
            {
                throw new ArgumentNullException(nameof(optionsMonitor));
            }

            this.webhookProcessor = webhookProcessor;
            this.logger = logger;
            this.options = optionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Webhook endpoint action.
        /// </summary>
        /// <param name="payload">Webhook payload.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] WebhookPayload payload)
        {
            // Options is injected as a singleton. This is not a good hack, but need to pass the host name and port
            this.logger.LogInformation($"Received webhook request: {JsonConvert.SerializeObject(payload)}");

            await this.webhookProcessor.ProcessWebhookNotificationAsync(payload).ConfigureAwait(false);

            return this.Ok();
        }
    }
}