using FluiTec.AppFx.Data;

namespace EdgeWorks.Data.Auctions
{
    [EntityName("AuctionFile")]
    public class AuctionFile : IEntity<int>
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public long LastModified { get; set; }
    }
}
