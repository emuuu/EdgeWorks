using Dapper;
using EdgeWorks.Data.Statistics;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdgeWorks.Data.Statistics
{
    public class StatisticsRepository : DapperRepository<ItemStatistic, int>, IStatisticsRepository
    {
        public StatisticsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            UnitOfWork.Connection.Execute("CREATE TABLE IF NOT EXISTS 'ItemStatistic' ( 'Id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 'Statisticname' TEXT NOT NULL, 'Extension' TEXT, 'Hash' TEXT, 'Path' TEXT, 'IsSuccess' INTEGER NOT NULL, 'ErrorMessage' TEXT )", transaction: UnitOfWork.Transaction);
        }

        public IEnumerable<ItemStatistic> GetByItem(long itemID)
        {
            return UnitOfWork.Connection.Query<ItemStatistic>("SELECT * FROM ItemStatistic WHERE ItemID = @ItemID", new { ItemID = itemID }, transaction: UnitOfWork.Transaction);
        }
    }
}
