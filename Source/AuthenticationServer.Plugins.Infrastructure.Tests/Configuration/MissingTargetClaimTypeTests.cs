using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.Configuration
{
    [TestClass]
    public class MissingTargetClaimTypeTests : ConfigurationTestsBase
    {
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void MissingTargetClaimType()
        {
            SetupFederatedAuthenticationConfiguration("MissingTargetClaimType.config");
        }
    }
}