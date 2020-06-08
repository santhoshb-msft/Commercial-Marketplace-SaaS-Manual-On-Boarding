using System;

namespace CommandCenter.Marketplace
{
    public class MarketplaceClientOptions
    {
        public const string MarketplaceClient = "MarketplaceClient";

        public Guid TenantId { get; set; }

        public Guid ClientId { get; set; }

        public string AppKey { get; set; }
    }
}