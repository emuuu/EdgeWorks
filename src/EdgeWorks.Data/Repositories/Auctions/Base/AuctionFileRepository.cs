using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using System.Threading.Tasks;

namespace EdgeWorks.Data.Auctions
{
    public class AuctionFileRepository : DapperRepository<AuctionFile, int>, IAuctionFileRepository
    {
        public AuctionFileRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<AuctionFile> GetByLastModified(long lastModified)
        {
            return await UnitOfWork.Connection.QueryFirstOrDefaultAsync<AuctionFile>("SELECT * FROM AuctionFile WHERE LastModified = @LastModiefied", new { LastModiefied = lastModified }, transaction: UnitOfWork.Transaction);
        }
    }
}
