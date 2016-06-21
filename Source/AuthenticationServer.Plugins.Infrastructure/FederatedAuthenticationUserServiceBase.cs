using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Affecto.AuthenticationServer.Plugins.Infrastructure.Configuration;
using IdentityServer3.Core.Models;

namespace Affecto.AuthenticationServer.Plugins.Infrastructure
{
    public abstract class FederatedAuthenticationUserServiceBase : UserServiceBase
    {
        private readonly Lazy<IFederatedAuthenticationConfiguration> federatedAuthenticationConfiguration;

        protected FederatedAuthenticationUserServiceBase(Lazy<IFederatedAuthenticationConfiguration> federatedAuthenticationConfiguration)
        {
            if (federatedAuthenticationConfiguration == null)
            {
                throw new ArgumentNullException(nameof(federatedAuthenticationConfiguration));
            }
            this.federatedAuthenticationConfiguration = federatedAuthenticationConfiguration;
        }

        /// <summary>
        /// This method gets called when the user uses an external identity provider to authenticate.
        /// The user's identity from the external provider is passed via the `externalUser` parameter which contains the
        /// provider identifier, the provider's identifier for the user, and the claims from the provider for the external user.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns/>
        public override Task AuthenticateExternalAsync(ExternalAuthenticationContext context)
        {
            Claim userAccountName = context.ExternalIdentity.Claims.SingleOrDefault(c => c.Type == federatedAuthenticationConfiguration.Value.UserAccountNameClaim);
            IReadOnlyCollection<KeyValuePair<string, string>> receivedClaims = MapReceivedClaims(context.ExternalIdentity.Claims);

            if (userAccountName != null)
            {
                CreateOrUpdateExternallyAuthenticatedUser(receivedClaims);
                context.AuthenticateResult = CreateAuthenticateResult(userAccountName.Value, AuthenticationTypes.Federation, receivedClaims,
                    context.SignInMessage.IdP);
            }
            else
            {
                context.AuthenticateResult = new AuthenticateResult("Required claims were missing from the IDP's message.");
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Override this method if you need to update externally authenticated user's information to another identity management system.
        /// </summary>
        /// <param name="receivedClaims">Claims received from external identity provider. Claim names are mapped according to configuration.</param>
        protected virtual void CreateOrUpdateExternallyAuthenticatedUser(IReadOnlyCollection<KeyValuePair<string, string>> receivedClaims)
        {
        }

        protected virtual IReadOnlyCollection<KeyValuePair<string, string>> MapReceivedClaims(IEnumerable<Claim> receivedClaims)
        {
            var mappedClaims = new List<KeyValuePair<string, string>>();
            IReceivedClaims configuration = federatedAuthenticationConfiguration.Value.ReceivedClaims;

            foreach (Claim claim in receivedClaims)
            {
                if (configuration.ContainsReceivedClaimType(claim.Type))
                {
                    string targetClaimType = configuration.GetTargetClaimType(claim.Type);
                    mappedClaims.Add(new KeyValuePair<string, string>(targetClaimType, claim.Value));
                }
            }

            return mappedClaims;
        }
    }
}