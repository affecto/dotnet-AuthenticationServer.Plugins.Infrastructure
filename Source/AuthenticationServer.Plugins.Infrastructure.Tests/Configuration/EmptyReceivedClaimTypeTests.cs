using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.Configuration
{
    [TestClass]
    public class EmptyReceivedClaimTypeTests : ConfigurationTestsBase
    {
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void EmptyReceivedClaimType()
        {
            SetupFederatedAuthenticationConfiguration("EmptyReceivedClaimType.config");
        }
    }
}