using System.Collections.Generic;
using System.Linq;

namespace Affecto.AuthenticationServer.Plugins.Infrastructure.Configuration
{
    internal class ReceivedClaims : List<IReceivedClaim>, IReceivedClaims
    {
        public ReceivedClaims()
        {
        }

        public ReceivedClaims(IEnumerable<IReceivedClaim> collection) : base(collection)
        {
        }

        public bool ContainsReceivedClaimType(string receivedClaimType)
        {
            return GetTargetClaimType(receivedClaimType) != null;
        }

        public string GetTargetClaimType(string receivedClaimType)
        {
            return this.Where(claim => claim.ReceivedClaimType.Equals(receivedClaimType)).Select(claim => claim.TargetClaimType).SingleOrDefault();
        }
    }
}