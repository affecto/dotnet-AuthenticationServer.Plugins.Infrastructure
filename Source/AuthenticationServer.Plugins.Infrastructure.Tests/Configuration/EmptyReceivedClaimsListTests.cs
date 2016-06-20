using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.Configuration
{
    [TestClass]
    public class EmptyReceivedClaimsListTests : ConfigurationTestsBase
    {
        [TestInitialize]
        public void Setup()
        {
            SetupFederatedAuthenticationConfiguration("EmptyReceivedClaimsList.config");
        }

        [TestMethod]
        public void ReceivedClaimsListIsEmpty()
        {
            Assert.IsNotNull(federatedAuthenticationConfiguration.ReceivedClaims);
            Assert.AreEqual(0, federatedAuthenticationConfiguration.ReceivedClaims.Count);
        }
    }
}