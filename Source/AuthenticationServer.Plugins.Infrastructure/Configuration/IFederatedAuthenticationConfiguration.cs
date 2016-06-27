namespace Affecto.AuthenticationServer.Plugins.Infrastructure.Configuration
{
    public interface IFederatedAuthenticationConfiguration
    {
        string UserAccountNameClaim { get; set; }
        IReceivedClaims ReceivedClaims { get; }
    }
}