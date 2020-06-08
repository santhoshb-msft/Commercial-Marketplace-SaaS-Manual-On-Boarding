using System.Threading;
using System.Threading.Tasks;
using CommandCenter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Marketplace;
using Microsoft.Marketplace.Models;

namespace CommandCenter.Controllers
{
    [Authorize("CommandCenterAdmin")]
    public class MailLinkController : Controller
    {
        private readonly IMarketplaceClient marketplaceClient;

        public MailLinkController(IMarketplaceClient marketplaceClient)
        {
            this.marketplaceClient = marketplaceClient;
        }

        [HttpGet]
        public async Task<IActionResult> Activate(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await this.marketplaceClient.Fulfillment.ActivateSubscriptionAsync(
                             notificationModel.SubscriptionId,
                             null,
                             null,
                             notificationModel.PlanId,
                             null,
                             cancellationToken);

            return this.View(
                           new ActivateActionViewModel
                               {
                                   SubscriptionId = notificationModel.SubscriptionId, PlanId = notificationModel.PlanId
                               });
        }

        [HttpGet]
        public async Task<IActionResult> QuantityChange(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await this.UpdateOperationAsync(notificationModel, cancellationToken);

            return this.View("OperationUpdate", notificationModel);
        }

        [HttpGet]
        public async Task<IActionResult> Reinstate(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await this.UpdateOperationAsync(notificationModel, cancellationToken);

            return this.View("OperationUpdate", notificationModel);
        }

        [HttpGet]
        public async Task<IActionResult> SuspendSubscription(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await this.UpdateOperationAsync(notificationModel, cancellationToken);

            return this.View("OperationUpdate", notificationModel);
        }

        [HttpGet]
        public async Task<IActionResult> Unsubscribe(
            NotificationModel notificationModel,
            CancellationToken cancellationToken)
        {
            await this.UpdateOperationAsync(notificationModel, cancellationToken);

            return this.View("OperationUpdate", notificationModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(NotificationModel notificationModel)
        {
            var result = await this.marketplaceClient.Fulfillment.UpdateSubscriptionAsync(
                             notificationModel.SubscriptionId,
                             null,
                             null,
                             notificationModel.PlanId,
                             null,
                             CancellationToken.None);

            return this.View(
                           new ActivateActionViewModel
                               {
                                   SubscriptionId = notificationModel.SubscriptionId, PlanId = notificationModel.PlanId
                               });
        }

        private async Task UpdateOperationAsync(
            NotificationModel payload,
            CancellationToken cancellationToken)
        {
            await this.marketplaceClient.SubscriptionOperations.UpdateOperationStatusAsync(
                       payload.SubscriptionId,
                       payload.OperationId,
                       null,
                       null,
                       payload.PlanId,
                       payload.Quantity,
                       UpdateOperationStatusEnum.Success,
                       cancellationToken);
        }
    }
}