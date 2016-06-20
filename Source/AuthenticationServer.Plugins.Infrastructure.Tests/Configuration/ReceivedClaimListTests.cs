using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.Configuration
{
    [TestClass]
    public class ReceivedClaimListTests : ConfigurationTestsBase
    {
        [TestInitialize]
        public void Setup()
        {
            SetupFederatedAuthenticationConfiguration("ReceivedClaimList.config");
        }

        [TestMethod]
        public void ReceivedClaimsAreRetrieved()
        {
            Assert.IsNotNull(federatedAuthenticationConfiguration.ReceivedClaims);
            Assert.AreEqual(3, federatedAuthenticationConfiguration.ReceivedClaims.Count);

            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/received", federatedAuthenticationConfiguration.ReceivedClaims.ElementAt(0).ReceivedClaimType);
            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/target", federatedAuthenticationConfiguration.ReceivedClaims.ElementAt(0).TargetClaimType);

            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/received2", federatedAuthenticationConfiguration.ReceivedClaims.ElementAt(1).ReceivedClaimType);
            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/target2", federatedAuthenticationConfiguration.ReceivedClaims.ElementAt(1).TargetClaimType);

            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/received3", federatedAuthenticationConfiguration.ReceivedClaims.ElementAt(2).ReceivedClaimType);
            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/target3", federatedAuthenticationConfiguration.ReceivedClaims.ElementAt(2).TargetClaimType);
        }

        [TestMethod]
        public void ReceivedClaimsContainsClaim()
        {
            Assert.IsTrue(federatedAuthenticationConfiguration.ReceivedClaims.ContainsReceivedClaimType("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/received3"));
        }

        [TestMethod]
        public void TargetClaimTypeIsRetrieved()
        {
            Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/target2", federatedAuthenticationConfiguration.ReceivedClaims.GetTargetClaimType("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/received2"));
        }
    }
}