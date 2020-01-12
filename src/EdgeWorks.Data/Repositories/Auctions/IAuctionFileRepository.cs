using FluiTec.AppFx.Data;
using System.Threading.Tasks;

namespace EdgeWorks.Data.Auctions
{
    public interface IAuctionFileRepository : IDataRepository<AuctionFile, int>
    {
        Task<AuctionFile> GetByLastModified(long lastModified);
    }
}
