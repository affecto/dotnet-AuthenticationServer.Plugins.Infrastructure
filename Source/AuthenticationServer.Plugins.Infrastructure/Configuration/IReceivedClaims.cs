using System.Collections.Generic;

namespace Affecto.AuthenticationServer.Plugins.Infrastructure.Configuration
{
    public interface IReceivedClaims : IReadOnlyCollection<IReceivedClaim>
    {
        bool ContainsReceivedClaimType(string receivedClaimType);
        string GetTargetClaimType(string receivedClaimType);
    }
}