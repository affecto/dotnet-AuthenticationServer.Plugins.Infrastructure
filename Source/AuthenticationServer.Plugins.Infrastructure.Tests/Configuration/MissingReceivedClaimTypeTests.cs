using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.Configuration
{
    [TestClass]
    public class MissingReceivedClaimTypeTests : ConfigurationTestsBase
    {
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void MissingReceivedClaimType()
        {
            SetupFederatedAuthenticationConfiguration("MissingReceivedClaimType.config");
        }
    }
}