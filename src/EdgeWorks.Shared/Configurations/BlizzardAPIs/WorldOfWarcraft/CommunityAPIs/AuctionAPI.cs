using Microsoft.Extensions.Options;

namespace EdgeWorks.Shared.Configurations.BlizzardAPIs.CommunityAPIs
{
    public class AuctionAPI
    {
        private ApiSettings _settings;

        public AuctionAPI(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        public string RequestUrl
        {
            get
            {
                return string.Format("https://{0}.api.blizzard.com/wow/auction/data/{1}?locale={2}", _settings.Region, _settings.Realm, _settings.Locale);
            }
        }
    }
}
