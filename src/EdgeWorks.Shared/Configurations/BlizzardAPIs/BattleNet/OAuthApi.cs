using Microsoft.Extensions.Options;

namespace EdgeWorks.Shared.Configurations.BlizzardAPIs.BattleNet
{
    public class OAuthApi
    {
        private ApiSettings _settings;

        public OAuthApi(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        public string AuthorizationRequest
        {
            get
            {
                return string.Format("https://{0}.battle.net/oauth/authorize", _settings.Region);
            }
        }

        public string AccessTokenRequest
        {
            get
            {
                return string.Format("https://{0}.battle.net/oauth/token", _settings.Region);
            }
        }
    }
}
