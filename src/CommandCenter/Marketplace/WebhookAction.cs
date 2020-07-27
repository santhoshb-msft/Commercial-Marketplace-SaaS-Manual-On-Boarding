namespace CommandCenter.Marketplace
{
    /// <summary>
    /// Webhook action.
    /// </summary>
    public enum WebhookAction
    {
        /// <summary>
        /// (When the resource has been deleted)
        /// </summary>
        Unsubscribe,

        /// <summary>
        /// (When the change plan operation has completed)
        /// </summary>
        ChangePlan,

        /// <summary>
        /// (When the change quantity operation has completed),
        /// </summary>
        ChangeQuantity,

        /// <summary>
        /// (When resource has been suspended)
        /// </summary>
        Suspend,

        /// <summary>
        /// (When resource has been reinstated after suspension)
        /// </summary>
        Reinstate,

        /// <summary>
        /// Transfer.
        /// </summary>
        Transfer,
    }
}