using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CommandCenter.Authorization;
using CommandCenter.Marketplace;
using CommandCenter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Marketplace;
using Microsoft.Marketplace.Models;

namespace CommandCenter.Controllers
{
    [Authorize]
    public class LandingPageController : Controller
    {
        private readonly ILogger<LandingPageController> logger;
        private readonly IMarketplaceClient marketplaceClient;
        private readonly IMarketplaceNotificationHandler notificationHandler;

        private readonly CommandCenterOptions options;

        public LandingPageController(
            IOptionsMonitor<CommandCenterOptions> commandCenterOptions,
            IMarketplaceClient marketplaceClient,
            IMarketplaceNotificationHandler notificationHandler,
            ILogger<LandingPageController> logger)
        {
            this.marketplaceClient = marketplaceClient;
            this.notificationHandler = notificationHandler;
            this.logger = logger;
            options = commandCenterOptions.CurrentValue;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(
            AzureSubscriptionProvisionModel provisionModel,
            CancellationToken cancellationToken)
        {
            var urlBase = $"{Request.Scheme}://{Request.Host}";
            options.BaseUrl = urlBase;
            try
            {
                await ProcessLandingPageAsync(provisionModel, cancellationToken);

                return RedirectToAction(nameof(Success));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: LandingPage
        public async Task<ActionResult> Index(string token, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError(string.Empty, "Token URL parameter cannot be empty");
                return View();
            }

            var provisioningModel = await BuildLandingPageModel(token, cancellationToken);

            if (provisioningModel != default)
            {
                provisioningModel.FullName = (User.Identity as ClaimsIdentity)?.FindFirst("name")?.Value;
                provisioningModel.Email = User.Identity.GetUserEmail();
                provisioningModel.BusinessUnitContactEmail = User.Identity.GetUserEmail();

                return View(provisioningModel);
            }

            ModelState.AddModelError(string.Empty, "Cannot resolve subscription");
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        private async Task<AzureSubscriptionProvisionModel> BuildLandingPageModel(
            string token,
            CancellationToken cancellationToken)
        {
            var resolvedSubscription = await marketplaceClient.Fulfillment.ResolveAsync(
                token,
                null,
                null,
                cancellationToken);

            if (resolvedSubscription == default(ResolvedSubscription)) return default;

            var existingSubscription = resolvedSubscription.Subscription;

            var availablePlans = await marketplaceClient.Fulfillment.ListAvailablePlansAsync(
                resolvedSubscription.Id.Value,
                null,
                null,
                cancellationToken);

            var pendingOperations = await marketplaceClient.SubscriptionOperations.ListOperationsAsync(
                resolvedSubscription.Id.Value,
                null,
                null,
                cancellationToken);

            var provisioningModel = new AzureSubscriptionProvisionModel
            {
                PlanId = resolvedSubscription.PlanId,
                SubscriptionId = resolvedSubscription.Id.Value,
                OfferId = resolvedSubscription.OfferId,
                SubscriptionName = resolvedSubscription.SubscriptionName,
                PurchaserEmail = existingSubscription.Purchaser.EmailId,
                PurchaserTenantId = existingSubscription.Purchaser.TenantId ?? Guid.Empty,

                // Assuming this will be set to the value the customer already set when subscribing, if we are here after the initial subscription activation
                // Landing page is used both for initial provisioning and configuration of the subscription.
                Region = TargetContosoRegionEnum.NorthAmerica,
                AvailablePlans = availablePlans.Plans,
                SubscriptionStatus = existingSubscription.SaasSubscriptionStatus ?? SubscriptionStatusEnum.NotStarted,
                PendingOperations = pendingOperations.Operations.Any(o => o.Status == OperationStatusEnum.InProgress)
            };

            return provisioningModel;
        }

        private async Task ProcessLandingPageAsync(
            AzureSubscriptionProvisionModel provisionModel,
            CancellationToken cancellationToken)
        {
            // A new subscription will have PendingFulfillmentStart as status
            if (provisionModel.SubscriptionStatus == SubscriptionStatusEnum.PendingFulfillmentStart)
                await notificationHandler.ProcessActivateAsync(provisionModel, cancellationToken);
            else
                await notificationHandler.ProcessChangePlanAsync(provisionModel, cancellationToken);
        }
    }
}