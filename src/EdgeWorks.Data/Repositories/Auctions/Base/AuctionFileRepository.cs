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
            UnitOfWork.Connection.Execute("CREATE TABLE IF NOT EXISTS 'AuctionFile' ( 'Id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 'Url' TEXT NOT NULL, 'LastModified' INTEGER NOT NULL )", transaction: UnitOfWork.Transaction);
        }

        public async Task<AuctionFile> GetByLastModified(long lastModified)
        {
            return await UnitOfWork.Connection.QueryFirstOrDefaultAsync<AuctionFile>("SELECT * FROM AuctionFile WHERE LastModified = @LastModiefied", new { LastModiefied = lastModified }, transaction: UnitOfWork.Transaction);
        }
    }
}
