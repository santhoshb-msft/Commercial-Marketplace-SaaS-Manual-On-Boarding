using System.Threading.Tasks;
using CommandCenter.Webhook;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CommandCenter.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [RequireHttps]
    //[AllowAnonymous]
    public class WebHookController : Controller
    {
        private readonly ILogger<WebHookController> logger;

        private readonly CommandCenterOptions options;

        private readonly IWebhookProcessor webhookProcessor;

        public WebHookController(
            IWebhookProcessor webhookProcessor,
            IOptionsMonitor<CommandCenterOptions> optionsMonitor,
            ILogger<WebHookController> logger)
        {
            this.webhookProcessor = webhookProcessor;
            this.logger = logger;
            options = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] WebhookPayload payload)
        {
            // Options is injected as a singleton. This is not a good hack, but need to pass the host name and port
            logger.LogInformation($"Received webhook request: {JsonConvert.SerializeObject(payload)}");

            await webhookProcessor.ProcessWebhookNotificationAsync(payload);

            return Ok();
        }
    }
}