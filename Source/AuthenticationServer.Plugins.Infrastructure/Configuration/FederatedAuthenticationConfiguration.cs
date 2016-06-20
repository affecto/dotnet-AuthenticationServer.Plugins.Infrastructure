using System.Configuration;
using Affecto.Configuration.Extensions;

namespace Affecto.AuthenticationServer.Plugins.Infrastructure.Configuration
{
    public class FederatedAuthenticationConfiguration : ConfigurationSection, IFederatedAuthenticationConfiguration
    {
        private static readonly FederatedAuthenticationConfiguration SettingsInstance =
            ConfigurationManager.GetSection("federatedAuthentication") as FederatedAuthenticationConfiguration;

        public static IFederatedAuthenticationConfiguration Settings
        {
            get { return SettingsInstance; }
        }

        [ConfigurationProperty("userAccountNameClaim", IsRequired = true)]
        public string UserAccountNameClaim
        {
            get { return (string) this["userAccountNameClaim"]; }
            set { this["userAccountNameClaim"] = value; }
        }

        public IReceivedClaims ReceivedClaims
        {
            get
            {
                if (ReceivedClaimsInternal != null)
                {
                    return new ReceivedClaims(ReceivedClaimsInternal);
                }

                return new ReceivedClaims();
            }
        }

        [ConfigurationProperty("receivedClaims", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ConfigurationElementCollection<ReceivedClaimConfiguration>), AddItemName = "receivedClaim")]
        private ConfigurationElementCollection<ReceivedClaimConfiguration> ReceivedClaimsInternal
        {
            get { return (ConfigurationElementCollection<ReceivedClaimConfiguration>) base["receivedClaims"]; }
        }

        protected override void PostDeserialize()
        {
            if (string.IsNullOrWhiteSpace(UserAccountNameClaim))
            {
                throw new ConfigurationErrorsException("User account name claim is required.");
            }
        }
    }
}