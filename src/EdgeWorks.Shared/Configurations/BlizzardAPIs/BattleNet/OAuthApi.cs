using Microsoft.Extensions.Options;

namespace EdgeWorks.Shared.Configurations.BlizzardAPIs.BattleNet
{
    public class OAuthApi
    {
        private string _region;

        public OAuthApi(IOptions<ApiSettings> settings)
        {
            _region = settings.Value.Region;
        }

        public string AuthorizationRequest
        {
            get
            {
                return string.Format("https://{0}.battle.net/oauth/authorize", _region);
            }
        }

        public string AccessTokenRequest
        {
            get
            {
                return string.Format("https://{0}.battle.net/oauth/token", _region);
            }
        }
    }
}
