using System.Configuration;
using Affecto.Configuration.Extensions;

namespace Affecto.AuthenticationServer.Plugins.Infrastructure.Configuration
{
    public class ReceivedClaimConfiguration : ConfigurationElementBase, IReceivedClaim
    {
        public override string ElementKey => ReceivedClaimType;

        [ConfigurationProperty("receivedClaimType", IsRequired = true)]
        public string ReceivedClaimType
        {
            get { return (string) this["receivedClaimType"]; }
            set { this["receivedClaimType"] = value; }
        }

        [ConfigurationProperty("targetClaimType", IsRequired = true)]
        public string TargetClaimType
        {
            get { return (string) this["targetClaimType"]; }
            set { this["targetClaimType"] = value; }
        }

        protected override void PostDeserialize()
        {
            if (string.IsNullOrWhiteSpace(ReceivedClaimType))
            {
                throw new ConfigurationErrorsException("Received claim type is required.");
            }

            if (string.IsNullOrWhiteSpace(TargetClaimType))
            {
                throw new ConfigurationErrorsException("Target claim type is required.");
            }
        }
    }
}