// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CommandCenter.Marketplace;
    using CommandCenter.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Marketplace.SaaS;
    using Microsoft.Marketplace.SaaS.Models;

    /// <summary>
    /// Only an admin can call this from an email.
    /// </summary>
    [Authorize("CommandCenterAdmin")]
    public class MailLinkController : Controller
    {
        private readonly IMarketplaceProcessor marketplaceProcessor;
        private readonly IMarketplaceClient marketplaceClient;

        public MailLinkController(IMarketplaceProcessor marketplaceProcessor, IMarketplaceClient marketplaceClient)
        {
            this.marketplaceProcessor = marketplaceProcessor;
            this.marketplaceClient = marketplaceClient;
        }

        /// <summary>
        /// Send an activate for the subscription.
        /// </summary>
        /// <param name="notificationModel">Notification details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<IActionResult> Activate(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await this.marketplaceProcessor.ActivateSubscriptionAsync(notificationModel.SubscriptionId, notificationModel.PlanId, cancellationToken);

            return this.View(
                new ActivateActionViewModel
                {
                    SubscriptionId = notificationModel.SubscriptionId, PlanId = notificationModel.PlanId,
                });
        }

        /// <summary>
        /// Acknowledge quantity change.
        /// </summary>
        /// <param name="notificationModel">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<IActionResult> QuantityChange(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await OperationAckAsync(notificationModel, cancellationToken);

            await this.UpdateOperationAsync(notificationModel, cancellationToken).ConfigureAwait(false);

            return this.View("OperationUpdate", notificationModel);
        }

        /// <summary>
        /// Acknowledge reinstate.
        /// </summary>
        /// <param name="notificationModel">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<IActionResult> Reinstate(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await OperationAckAsync(notificationModel, cancellationToken);

            return this.View("OperationUpdate", notificationModel);
        }

        /// <summary>
        /// Acknowledge suspend.
        /// </summary>
        /// <param name="notificationModel">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<IActionResult> SuspendSubscription(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await OperationAckAsync(notificationModel, cancellationToken);

            await this.UpdateOperationAsync(notificationModel, cancellationToken).ConfigureAwait(false);

            return this.View("OperationUpdate", notificationModel);
        }

        /// <summary>
        /// Acknowledge unsubscribe. This is not really necessariy as protocol states, but adding here to point out it is not necessary.
        /// </summary>
        /// <param name="notificationModel">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<IActionResult> Unsubscribe(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await OperationAckAsync(notificationModel, cancellationToken);

            return this.View("OperationUpdate", notificationModel);
        }

        /// <summary>
        /// Update request..
        /// </summary>
        /// <param name="notificationModel">Notification model.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Action result.</returns>
        [HttpGet]
        public async Task<IActionResult> Update(NotificationModel notificationModel, CancellationToken cancellationToken)
        {
            if (notificationModel == null)
            {
                throw new ArgumentNullException(nameof(notificationModel));
            }

            var result = await this.marketplaceClient.FulfillmentOperations.UpdateSubscriptionAsync(
                notificationModel.SubscriptionId,
                null,
                null,
                notificationModel.PlanId,
                null,
                cancellationToken).ConfigureAwait(false);

            return this.View(
                new ActivateActionViewModel
                {
                    SubscriptionId = notificationModel.SubscriptionId, PlanId = notificationModel.PlanId,
                });
        }

        private async Task OperationAckAsync(
            NotificationModel payload,
            CancellationToken cancellationToken)
        {
            await this.marketplaceProcessor.OperationAckAsync(payload.SubscriptionId, payload.OperationId, payload.PlanId, payload.Quantity, cancellationToken);
        }
    }
}