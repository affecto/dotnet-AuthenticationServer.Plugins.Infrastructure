using System.Collections.Generic;
using Affecto.AuthenticationServer.Plugins.Infrastructure;
using IdentityServer3.Core.Models;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.TestClasses
{
    internal class TestUserService : UserServiceBase
    {
        public string ReceivedUserName { get; private set; }
        public string ReceivedAuthenticationType { get; private set; }
        public IEnumerable<KeyValuePair<string, string>> ReceivedReceivedClaims { get; private set; }
        public string ReceivedIdentityProvider { get; private set; }

        public AuthenticateResult ResultToReturn { get; set; }
        public bool? PasswordMatchToReturn { get; set; }

        protected override AuthenticateResult CreateAuthenticateResult(string userName, string authenticationType,
            IReadOnlyCollection<KeyValuePair<string, string>> receivedClaims = null, string identityProvider = "idsrv")
        {
            ReceivedUserName = userName;
            ReceivedAuthenticationType = authenticationType;
            ReceivedReceivedClaims = receivedClaims;
            ReceivedIdentityProvider = identityProvider;

            return ResultToReturn;
        }

        protected override bool IsMatchingPassword(string userName, string password)
        {
            if (PasswordMatchToReturn.HasValue)
            {
                return PasswordMatchToReturn.Value;
            }

            return base.IsMatchingPassword(userName, password);
        }
    }
}