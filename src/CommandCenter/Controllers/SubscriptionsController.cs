using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandCenter.Marketplace;
using CommandCenter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Marketplace;
using Microsoft.Marketplace.Models;

namespace CommandCenter.Controllers
{
    [Authorize("CommandCenterAdmin")]
    public class SubscriptionsController : Controller
    {
        private readonly IMarketplaceClient marketplaceClient;

        private readonly IOperationsStore operationsStore;

        private readonly CommandCenterOptions options;

        public SubscriptionsController(
            IMarketplaceClient marketplaceClient,
            IOperationsStore operationsStore,
            IOptionsMonitor<CommandCenterOptions> options)
        {
            this.marketplaceClient = marketplaceClient;
            this.operationsStore = operationsStore;
            this.options = options.CurrentValue;
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {            
            var subscriptions = await this.marketplaceClient.Fulfillment.ListAllSubscriptionsAsync();

            var subscriptionsViewModel = subscriptions.Select(SubscriptionViewModel.FromSubscription)
                .Where(s => s.State != SubscriptionStatusEnum.Unsubscribed || this.options.ShowUnsubscribed);

            var newViewModel = new List<SubscriptionViewModel>();

            foreach (var subscription in subscriptionsViewModel)
            {
                // REMOVING THE FOLLOWING FOR THE SAKE OF PERFORMANCE, but keeping them here as reference

                //subscription.PendingOperations =
                //    (await this.fulfillmentClient.GetSubscriptionOperationsAsync(
                //         requestId,
                //         correlationId,
                //         subscription.SubscriptionId,
                //         cancellationToken)).Any(o => o.Status == OperationStatusEnum.InProgress);

                var recordedSubscriptionOperations =
                    await this.operationsStore.GetAllSubscriptionRecordsAsync(
                        subscription.SubscriptionId,
                        cancellationToken);

                // REMOVING THE FOLLOWING FOR THE SAKE OF PERFORMANCE, but keeping them here as reference

                //var subscriptionOperations = new List<SubscriptionOperation>();
                //foreach (var operation in recordedSubscriptionOperations)
                //{
                //    var subscriptionOperation = await this.fulfillmentClient.GetSubscriptionOperationAsync(
                //          operation.SubscriptionId,
                //          operation.OperationId,
                //          requestId,
                //          correlationId,
                //          cancellationToken);

                //    if (subscriptionOperation != default(SubscriptionOperation))
                //    {
                //        subscriptionOperations.Add(subscriptionOperation);
                //    }
                //}


                //subscription.PendingOperations |=
                //    subscriptionOperations.Any(o => o.Status == OperationStatusEnum.InProgress);

                subscription.ExistingOperations = (await this.operationsStore.GetAllSubscriptionRecordsAsync(
                    subscription.SubscriptionId,
                    cancellationToken)).Any();
                subscription.OperationCount = recordedSubscriptionOperations.Count();
                newViewModel.Add(subscription);
            }            

            return this.View(newViewModel.OrderByDescending(s => s.SubscriptionName));
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult NotAuthorized()
        {
            return this.View();
        }

        public async Task<IActionResult> Operations(Guid subscriptionId, CancellationToken cancellationToken)
        {
            var subscriptionOperations =
                await this.operationsStore.GetAllSubscriptionRecordsAsync(subscriptionId, cancellationToken);

            var subscription = await this.marketplaceClient.Fulfillment.GetSubscriptionAsync(subscriptionId, null, null, cancellationToken);

            var operations = new List<Operation>();

            foreach (var operation in subscriptionOperations)
                operations.Add(
                    await this.marketplaceClient.SubscriptionOperations.GetOperationStatusAsync(subscriptionId,
                        operation.OperationId,
                        null,
                        null,
                        cancellationToken));

            return this.View(new OperationsViewModel { SubscriptionName = subscription.Name, Operations = operations });
        }

        public async Task<IActionResult> SubscriptionAction(
            Guid subscriptionId,
            ActionsEnum subscriptionAction,
            CancellationToken cancellationToken)
        {

            switch (subscriptionAction)
            {
                case ActionsEnum.Activate:
                    break;

                case ActionsEnum.Update:
                    var availablePlans = await this.marketplaceClient.Fulfillment.ListAvailablePlansAsync(
                                              subscriptionId,
                                              null,
                                              null,
                                              cancellationToken);

                    var subscription = await this.marketplaceClient.Fulfillment.GetSubscriptionAsync(
                                           subscriptionId,
                                           null,
                                           null,
                                           cancellationToken);

                    var pendingOperations = await this.marketplaceClient.SubscriptionOperations.ListOperationsAsync(
                        subscriptionId,
                        null,
                        null,
                        cancellationToken);

                    var updateSubscriptionViewModel = new UpdateSubscriptionViewModel
                    {
                        SubscriptionId = subscriptionId,
                        SubscriptionName = subscription.Name,
                        CurrentPlan = subscription.PlanId,
                        AvailablePlans = availablePlans.Plans,
                        PendingOperations = pendingOperations.Operations.Any(
                                                                      o => o.Status == OperationStatusEnum.InProgress)
                    };

                    return this.View("UpdateSubscription", updateSubscriptionViewModel);

                case ActionsEnum.Ack:
                    break;

                case ActionsEnum.Unsubscribe:
                    await this.marketplaceClient.Fulfillment.DeleteSubscriptionAsync(
                                                subscriptionId,
                                                null,
                                                null,
                                                cancellationToken);

                    return this.RedirectToAction("Index");

                default:
                    throw new ArgumentOutOfRangeException(nameof(subscriptionAction), subscriptionAction, null);
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubscription(
            UpdateSubscriptionViewModel model,
            CancellationToken cancellationToken)
        {
            var pendingOperations = await this.marketplaceClient.SubscriptionOperations.ListOperationsAsync(
                model.SubscriptionId,
                null,
                null,
                cancellationToken);

            if (pendingOperations.Operations.Any(o => o.Status == OperationStatusEnum.InProgress)) return this.RedirectToAction("Index");
            var updateResult = await this.marketplaceClient.Fulfillment.UpdateSubscriptionAsync(
                                   model.SubscriptionId,
                                   null,
                                   null,
                                   model.NewPlan,
                                   null,
                                   cancellationToken);

            await this.operationsStore.RecordAsync(model.SubscriptionId, updateResult.OperationLocation);

            return this.RedirectToAction("Index");
        }
    }
}