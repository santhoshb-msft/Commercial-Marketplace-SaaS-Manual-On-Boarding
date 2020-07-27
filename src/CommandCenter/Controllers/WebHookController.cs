using System.Threading;
using System.Threading.Tasks;
using CommandCenter.Marketplace;
using CommandCenter.Webhook;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
        private readonly IMarketplaceProcessor marketplaceProcessor;
        private readonly ILogger<WebHookController> logger;

        private readonly CommandCenterOptions options;

        public WebHookController(
            IOptionsMonitor<CommandCenterOptions> optionsMonitor,
            IMarketplaceProcessor marketplaceProcessor,
            ILogger<WebHookController> logger)
        {
            this.marketplaceProcessor = marketplaceProcessor;
            this.logger = logger;
            this.options = optionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Webhook endpoint action.
        /// </summary>
        /// <param name="payload">Webhook payload.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] WebhookPayload payload, CancellationToken cancellationToken)
        {
            // Options is injected as a singleton. This is not a good hack, but need to pass the host name and port
            this.logger.LogInformation($"Received webhook request: {JsonConvert.SerializeObject(payload)}");

            await this.marketplaceProcessor.ProcessWebhookNotificationAsync(payload, cancellationToken);

            return this.Ok();
        }
    }
}