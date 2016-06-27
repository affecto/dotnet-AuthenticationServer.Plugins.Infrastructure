using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AuthenticationServer.Plugins.Infrastructure.Tests.TestClasses;
using IdentityServer3.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationServer.Plugins.Infrastructure.Tests
{
    [TestClass]
    public class UserServiceBaseTests
    {
        private TestUserService sut;
        private LocalAuthenticationContext context;
        private AuthenticateResult authenticateResult;

        [TestInitialize]
        public void Setup()
        {
            sut = new TestUserService();
            context = new LocalAuthenticationContext();
            authenticateResult = new AuthenticateResult("TEST");
        }

        [TestMethod]
        public void PasswordDoesNotMatchByDefault()
        {
            sut.ResultToReturn = authenticateResult;

            sut.AuthenticateLocalAsync(context);

            Assert.IsNull(context.AuthenticateResult);
        }

        [TestMethod]
        public void AuthenticateResultIsNotReturnedWhenPasswordDoesNotMatch()
        {
            sut.PasswordMatchToReturn = false;
            sut.ResultToReturn = authenticateResult;

            sut.AuthenticateLocalAsync(context);

            Assert.IsNull(context.AuthenticateResult);
        }

        [TestMethod]
        public void AuthenticateResultIsReturnedWhenPasswordMatches()
        {
            sut.PasswordMatchToReturn = true;
            sut.ResultToReturn = authenticateResult;

            sut.AuthenticateLocalAsync(context);

            Assert.AreSame(authenticateResult, context.AuthenticateResult);
        }

        [TestMethod]
        public void AuthenticateResultIsCreatedWithCorrectValues()
        {
            const string userName = "TestUser";

            context.UserName = userName;
            sut.PasswordMatchToReturn = true;
            sut.ResultToReturn = authenticateResult;

            sut.AuthenticateLocalAsync(context);

            Assert.AreSame(userName, sut.ReceivedUserName);
            Assert.AreSame(AuthenticationTypes.Password, sut.ReceivedAuthenticationType);
            Assert.IsNull(sut.ReceivedReceivedClaims);
            Assert.AreEqual("idsrv", sut.ReceivedIdentityProvider);
        }

        [TestMethod]
        public void ProfileDataContainsIssuedClaims()
        {
            var claims = new List<Claim>
            {
                new Claim("claim1", "value1"),
                new Claim("claim2", "value2")
            };

            var claimsIdentity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(new List<ClaimsIdentity> { claimsIdentity });
            var profileDataRequestContext = new ProfileDataRequestContext(claimsPrincipal, new Client(), null);

            sut.GetProfileDataAsync(profileDataRequestContext);

            IEnumerable<Claim> issuedClaims = profileDataRequestContext.IssuedClaims;
            Assert.IsNotNull(issuedClaims);
            Assert.AreEqual(2, issuedClaims.Count());
            Assert.AreEqual("claim1", issuedClaims.ElementAt(0).Type);
            Assert.AreEqual("claim2", issuedClaims.ElementAt(1).Type);
            Assert.AreEqual("value1", issuedClaims.ElementAt(0).Value);
            Assert.AreEqual("value2", issuedClaims.ElementAt(1).Value);
        }
    }
}