using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationServer.Plugins.Infrastructure.Tests.Configuration
{
    [TestClass]
    public class EmptyTargetClaimTypeTests : ConfigurationTestsBase
    {
        [TestMethod]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void EmptyTargetClaimType()
        {
            SetupFederatedAuthenticationConfiguration("EmptyTargetClaimType.config");
        }
    }
}