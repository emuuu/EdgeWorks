using EdgeWorks.Shared.Configurations.BlizzardAPIs;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.BattleNet;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EdgeWorks.Shared.Services.Authentication
{
    public class TokenService : ITokenService
    { 
        private readonly Client _client;
        private readonly OAuthApi _authorizeEndpoint;
        private readonly ILogger<TokenService> _logger;

        private TokenResponse _accesToken;
        private DateTime _tokenExpirationTime;

        public TokenService(IOptions<Client> client, OAuthApi api, ILogger<TokenService> logger)
        {
            _client = client.Value;
            _authorizeEndpoint = api;
            _logger = logger;
        }

        public async Task<string> GetAccessToken()
        {
            if (_accesToken == null || _tokenExpirationTime < DateTime.UtcNow)
            {
                _logger.LogTrace("Start retreiving new access token");

                var client = new HttpClient();
                var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _authorizeEndpoint.AccessTokenRequest,
                    ClientId = _client.ClientId,
                    ClientSecret = _client.ClientSecret
                });
                if (response.IsError)
                {
                    _logger.LogError("Failed retreiving new access token with reason {0}", response.Error);
                    throw new Exception(response.Error);
                }
                _tokenExpirationTime = DateTime.UtcNow.AddSeconds(response.ExpiresIn);
                _accesToken = response;
            }
            _logger.LogTrace("Successfully retreived access token");
            return _accesToken.AccessToken;
        }
    }
}
