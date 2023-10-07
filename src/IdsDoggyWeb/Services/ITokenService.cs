using IdentityModel.Client;
using IdentityServer4.Models;

namespace IdsDoggyWeb.Services
{
    public interface ITokenService

    {
        Task<TokenResponse> GetToken(string scope);
    }

    public class TokenService : ITokenService
    {
        private readonly DiscoveryDocumentResponse _discDocument;

        public TokenService()
        {
            using (var client = new HttpClient())
            {
                _discDocument = client.GetDiscoveryDocumentAsync("https://localhost:5001/.well-known/openid-configuration").Result;
            }
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discDocument.TokenEndpoint,
                    ClientId = "doggy.api",
                    Scope = scope,
                    ClientSecret = "secret"
                });
                if (tokenResponse.IsError)
                {
                    throw new Exception("Token Error");
                }
                return tokenResponse;
            }
        }
    }
}
