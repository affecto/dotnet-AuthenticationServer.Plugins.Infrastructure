using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.Configuration
{
    [TestClass]
    public class CompleteFederatedAuthenticationConfigurationTests : ConfigurationTestsBase
    {
        [TestInitialize]
        public void Setup()
        {
            SetupFederatedAuthenticationConfiguration("ValidConfiguration.config");
        }

        [TestMethod]
        public void UserAccountNameClaimIsRetrieved()
        {
            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", federatedAuthenticationConfiguration.UserAccountNameClaim);
        }

        [TestMethod]
        public void ReceivedClaimsAreRetrieved()
        {
            Assert.IsNotNull(federatedAuthenticationConfiguration.ReceivedClaims);
            Assert.AreEqual(1, federatedAuthenticationConfiguration.ReceivedClaims.Count);
            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/received", federatedAuthenticationConfiguration.ReceivedClaims.Single().ReceivedClaimType);
            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/target", federatedAuthenticationConfiguration.ReceivedClaims.Single().TargetClaimType);
        }

        [TestMethod]
        public void ReceivedClaimsContainsClaim()
        {
            Assert.IsTrue(federatedAuthenticationConfiguration.ReceivedClaims.ContainsReceivedClaimType("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/received"));
        }

        [TestMethod]
        public void TargetClaimTypeIsRetrieved()
        {
            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/target", federatedAuthenticationConfiguration.ReceivedClaims.GetTargetClaimType("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/received"));
        }
    }
}