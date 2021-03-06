using System;
using System.Collections.Generic;
using Affecto.AuthenticationServer.Plugins.Infrastructure;
using Affecto.AuthenticationServer.Plugins.Infrastructure.Configuration;
using IdentityServer3.Core.Models;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.TestClasses
{
    internal class TestFederatedAuthenticationUserService : FederatedAuthenticationUserServiceBase
    {
        public IEnumerable<KeyValuePair<string, string>> ReceivedReceivedClaimsForAuthenticatedUser { get; private set; }

        public string ReceivedUserName { get; private set; }
        public string ReceivedAuthenticationType { get; private set; }
        public IEnumerable<KeyValuePair<string, string>> ReceivedReceivedClaimsForAuthenticateResult { get; private set; }
        public string ReceivedIdentityProvider { get; private set; }

        public AuthenticateResult ResultToReturn { get; set; }

        public TestFederatedAuthenticationUserService(Lazy<IFederatedAuthenticationConfiguration> federatedAuthenticationConfiguration)
            : base(federatedAuthenticationConfiguration)
        {
        }

        protected override void CreateOrUpdateExternallyAuthenticatedUser(IReadOnlyCollection<KeyValuePair<string, string>> receivedClaims)
        {
            ReceivedReceivedClaimsForAuthenticatedUser = receivedClaims;
        }

        protected override AuthenticateResult CreateAuthenticateResult(string userName, string authenticationType,
            IReadOnlyCollection<KeyValuePair<string, string>> receivedClaims = null, string identityProvider = "idsrv")
        {
            ReceivedUserName = userName;
            ReceivedAuthenticationType = authenticationType;
            ReceivedReceivedClaimsForAuthenticateResult = receivedClaims;
            ReceivedIdentityProvider = identityProvider;

            return ResultToReturn;
        }
    }
}