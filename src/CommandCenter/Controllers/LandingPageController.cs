// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Controllers
{
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
    using Microsoft.Marketplace.SaaS;
    using Microsoft.Marketplace.SaaS.Models;

    /// <summary>
    /// Landing page.
    /// </summary>
    [Authorize]
    public class LandingPageController : Controller
    {
        private readonly ILogger<LandingPageController> logger;
        private readonly IMarketplaceProcessor marketplaceProcessor;
        private readonly IMarketplaceNotificationHandler notificationHandler;
        private readonly IMarketplaceClient marketplaceClient;
        private readonly CommandCenterOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="LandingPageController"/> class.
        /// </summary>
        /// <param name="commandCenterOptions">Options.</param>
        /// <param name="marketplaceClient">Marketplace API client.</param>
        /// <param name="notificationHandler">Notification handler.</param>
        /// <param name="logger">Logger.</param>
        public LandingPageController(
            IOptionsMonitor<CommandCenterOptions> commandCenterOptions,
            IMarketplaceProcessor marketplaceProcessor,
            IMarketplaceNotificationHandler notificationHandler,
            IMarketplaceClient marketplaceClient,
            ILogger<LandingPageController> logger)
        {
            this.marketplaceProcessor = marketplaceProcessor;
            this.notificationHandler = notificationHandler;
            this.marketplaceClient = marketplaceClient;
            this.logger = logger;
            this.options = commandCenterOptions.CurrentValue;
        }

        // GET: LandingPage
        public async Task<ActionResult> Index(string token, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(token))
            {
                this.ModelState.AddModelError(string.Empty, "Token URL parameter cannot be empty");
                return this.View();
            }

            // Get the subscription for the offer from the marketplace purchase identification token
            var resolvedSubscription = await this.marketplaceProcessor.GetSubscriptionFromPurchaseIdentificationTokenAsync(token, cancellationToken);

            // Rest is implementation detail. In this sample, we chose allow the subscriber to change the plan for an activated subscriptio
            if (resolvedSubscription == default(ResolvedSubscription)) return default;

            var existingSubscription = resolvedSubscription.Subscription;

            var availablePlans = await this.marketplaceClient.FulfillmentOperations.ListAvailablePlansAsync(
                resolvedSubscription.Id.Value,
                null,
                null,
                cancellationToken).ConfigureAwait(false);

            var pendingOperations = await this.marketplaceClient.SubscriptionOperations.ListOperationsAsync(
                resolvedSubscription.Id.Value,
                null,
                null,
                cancellationToken).ConfigureAwait(false);

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
                PendingOperations = pendingOperations.Operations.Any(o => o.Status == OperationStatusEnum.InProgress),
            };

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(AzureSubscriptionProvisionModel provisionModel, CancellationToken cancellationToken)
        {
            var urlBase = $"{Request.Scheme}://{Request.Host}";
            options.BaseUrl = urlBase;
            try
            {
                // A new subscription will have PendingFulfillmentStart as status
                if (provisionModel.SubscriptionStatus == SubscriptionStatusEnum.PendingFulfillmentStart)
                    await notificationHandler.ProcessNewSubscriptionAsyc(provisionModel, cancellationToken);
                else
                    await notificationHandler.ProcessChangePlanAsync(provisionModel, cancellationToken);

                return RedirectToAction(nameof(Success));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}