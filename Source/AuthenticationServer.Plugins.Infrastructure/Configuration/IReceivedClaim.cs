namespace Affecto.AuthenticationServer.Plugins.Infrastructure.Configuration
{
    public interface IReceivedClaim
    {
        string ReceivedClaimType { get; }
        string TargetClaimType { get; }
    }
}