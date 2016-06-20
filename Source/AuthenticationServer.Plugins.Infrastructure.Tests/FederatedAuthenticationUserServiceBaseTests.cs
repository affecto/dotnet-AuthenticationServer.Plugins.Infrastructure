using System;
using System.Collections.Generic;
using System.Security.Claims;
using Affecto.AuthenticationServer.Plugins.Infrastructure.Configuration;
using AuthenticationServer.Plugins.Infrastructure.Tests.TestClasses;
using IdentityServer3.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace AuthenticationServer.Plugins.Infrastructure.Tests
{
    [TestClass]
    public class FederatedAuthenticationUserServiceBaseTests
    {
        private TestFederatedAuthenticationUserService sut;
        private IFederatedAuthenticationConfiguration configuration;
        private ExternalAuthenticationContext context;
        private AuthenticateResult authenticateResult;

        [TestInitialize]
        public void Setup()
        {
            configuration = Substitute.For<IFederatedAuthenticationConfiguration>();
            configuration.UserAccountNameClaim.Returns("userName");

            context = new ExternalAuthenticationContext
            {
                ExternalIdentity = ExternalIdentity.FromClaims(new List<Claim>
                {
                    new Claim("sub", "testsub"),
                    new Claim("userName", "test user"),
                    new Claim("mappedClaim1", "value1"),
                    new Claim("unmappedClaim", "value3"),
                    new Claim("mappedClaim2", "value2")
                }),
                SignInMessage = new SignInMessage { IdP = "TestIdp" }
            };

            authenticateResult = new AuthenticateResult("TEST");

            sut = new TestFederatedAuthenticationUserService(new Lazy<IFederatedAuthenticationConfiguration>(() => configuration));
            sut.ResultToReturn = authenticateResult;
        }

        [TestMethod]
        public void AbsenceOfUserNameResultsToError()
        {
            context.ExternalIdentity = ExternalIdentity.FromClaims(new List<Claim> { new Claim("sub", "testsub") });

            sut.AuthenticateExternalAsync(context);

            Assert.IsNotNull(context.AuthenticateResult);
            Assert.IsTrue(context.AuthenticateResult.IsError);
        }

        [TestMethod]
        public void AuthenticateResultIsReturnedWithEmptyClaims()
        {
            sut.AuthenticateExternalAsync(context);

            Assert.AreSame(authenticateResult, context.AuthenticateResult);
            Assert.AreSame("test user", sut.ReceivedUserName);
            Assert.AreSame(AuthenticationTypes.Federation, sut.ReceivedAuthenticationType);
            Assert.IsNotNull(sut.ReceivedReceivedClaimsForAuthenticateResult);
            Assert.AreEqual(0, sut.ReceivedReceivedClaimsForAuthenticateResult.Count);
            Assert.AreEqual("TestIdp", sut.ReceivedIdentityProvider);
        }

        [TestMethod]
        public void ExternalUserIsUpdatedWithEmptyClaims()
        {
            sut.AuthenticateExternalAsync(context);

            Assert.IsNotNull(sut.ReceivedReceivedClaimsForAuthenticatedUser);
            Assert.AreEqual(0, sut.ReceivedReceivedClaimsForAuthenticatedUser.Count);
        }

        [TestMethod]
        public void AuthenticateResultIsReturnedWithMappedClaims()
        {
            configuration.ReceivedClaims.ContainsReceivedClaimType("mappedClaim1").Returns(true);
            configuration.ReceivedClaims.ContainsReceivedClaimType("mappedClaim2").Returns(true);
            configuration.ReceivedClaims.GetTargetClaimType("mappedClaim1").Returns("mappedTargetClaim1");
            configuration.ReceivedClaims.GetTargetClaimType("mappedClaim2").Returns("mappedTargetClaim2");

            sut.AuthenticateExternalAsync(context);

            Assert.IsNotNull(sut.ReceivedReceivedClaimsForAuthenticateResult);
            Assert.AreEqual(2, sut.ReceivedReceivedClaimsForAuthenticateResult.Count);
            Assert.AreEqual("value1", sut.ReceivedReceivedClaimsForAuthenticateResult["mappedTargetClaim1"]);
            Assert.AreEqual("value2", sut.ReceivedReceivedClaimsForAuthenticateResult["mappedTargetClaim2"]);
        }

        [TestMethod]
        public void ExternalUserIsUpdatedWithMappedClaims()
        {
            configuration.ReceivedClaims.ContainsReceivedClaimType("mappedClaim1").Returns(true);
            configuration.ReceivedClaims.ContainsReceivedClaimType("mappedClaim2").Returns(true);
            configuration.ReceivedClaims.GetTargetClaimType("mappedClaim1").Returns("mappedTargetClaim1");
            configuration.ReceivedClaims.GetTargetClaimType("mappedClaim2").Returns("mappedTargetClaim2");

            sut.AuthenticateExternalAsync(context);

            Assert.IsNotNull(sut.ReceivedReceivedClaimsForAuthenticatedUser);
            Assert.AreEqual(2, sut.ReceivedReceivedClaimsForAuthenticatedUser.Count);
            Assert.AreEqual("value1", sut.ReceivedReceivedClaimsForAuthenticatedUser["mappedTargetClaim1"]);
            Assert.AreEqual("value2", sut.ReceivedReceivedClaimsForAuthenticatedUser["mappedTargetClaim2"]);
        }
    }
}