using System.Collections.Generic;

namespace EdgeWorks.Models.Auctions
{
    public class AuctionData
    {
        public IEnumerable<Realm> Realms { get; set; }

        public IEnumerable<Auction> Auctions { get; set; }
    }
}
