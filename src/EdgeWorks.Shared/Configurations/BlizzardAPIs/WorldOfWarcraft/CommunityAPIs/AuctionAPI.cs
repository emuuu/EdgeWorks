using System;
using System.Collections.Generic;
using System.Text;

namespace EdgeWorks.Shared.Configurations.EdgeWorksAPIs.CommunityAPIs
{
    public class AuctionAPI
    {
        private string _region;
        private string _realm;
        private string _locale;

        public AuctionAPI(string region, string realm, string locale)
        {
            _region = region;
            _realm = realm;
            _locale = locale;
        }

        public string RequestUrl
        {
            get
            {
                return string.Format("https://{0}.api.blizzard.com/wow/auction/data/{1}?locale={2}", _region, _realm, _locale);
            }
        }
    }
}
