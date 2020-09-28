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
        private readonly IMarketplaceSaaSClient marketplaceClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailLinkController"/> class.
        /// </summary>
        /// <param name="marketplaceClient">Marketplace API client.</param>
        public MailLinkController(IMarketplaceSaaSClient marketplaceClient)
        {
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
            if (notificationModel == null)
            {
                throw new ArgumentNullException(nameof(notificationModel));
            }

            await this.marketplaceClient.FulfillmentOperations.ActivateSubscriptionAsync(
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
            if (notificationModel == null)
            {
                throw new ArgumentNullException(nameof(notificationModel));
            }

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
            if (notificationModel == null)
            {
                throw new ArgumentNullException(nameof(notificationModel));
            }

            await this.UpdateOperationAsync(notificationModel, cancellationToken).ConfigureAwait(false);

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
            if (notificationModel == null)
            {
                throw new ArgumentNullException(nameof(notificationModel));
            }

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
            if (notificationModel == null)
            {
                throw new ArgumentNullException(nameof(notificationModel));
            }

            await this.UpdateOperationAsync(notificationModel, cancellationToken).ConfigureAwait(false);

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

        private async Task UpdateOperationAsync(
            NotificationModel payload,
            CancellationToken cancellationToken)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            await this.marketplaceClient.SubscriptionOperations.UpdateOperationStatusAsync(
                payload.SubscriptionId,
                payload.OperationId,
                null,
                null,
                payload.PlanId,
                payload.Quantity,
                UpdateOperationStatusEnum.Success,
                cancellationToken).ConfigureAwait(false);
        }
    }
}