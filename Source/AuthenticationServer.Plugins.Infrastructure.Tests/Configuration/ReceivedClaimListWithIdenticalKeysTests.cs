using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.Configuration
{
    [TestClass]
    public class ReceivedClaimListWithIdenticalKeysTests : ConfigurationTestsBase
    {
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void ReceivedClaimTypesCannotBeSame()
        {
            SetupFederatedAuthenticationConfiguration("ReceivedClaimListWithIdenticalKeys.config");
        }
    }
}