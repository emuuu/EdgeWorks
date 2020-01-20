using Dapper;
using EdgeWorks.Data.Statistics;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdgeWorks.Data.Statistics
{
    public class StatisticsRepository : DapperRepository<ItemStatistic, int>, IStatisticsRepository
    {
        public StatisticsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            UnitOfWork.Connection.Execute("CREATE TABLE IF NOT EXISTS  'ItemStatistic' ( 'Id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 'ItemID' INTEGER NOT NULL, 'Name' TEXT NOT NULL, 'TimeStamp' TEXT NOT NULL, 'Sum' REAL NOT NULL, 'Count' INTEGER NOT NULL, 'ArithmeticMean' REAL NOT NULL, 'Median' REAL NOT NULL, 'Minimum' REAL NOT NULL, 'Maxmimum' REAL NOT NULL, 'Range' REAL NOT NULL, 'Variance' REAL NOT NULL, 'StandardDeviation' REAL NOT NULL, 'SampleVariance' REAL NOT NULL, 'SampleStandardDeviation' REAL NOT NULL, 'LowerQuartile' REAL NOT NULL, 'HigherQuartile' REAL NOT NULL, 'QuartileDistance' REAL NOT NULL )", transaction: UnitOfWork.Transaction);
        }

        public IEnumerable<ItemStatistic> GetByItem(long itemID)
        {
            return UnitOfWork.Connection.Query<ItemStatistic>("SELECT * FROM ItemStatistic WHERE ItemID = @ItemID", new { ItemID = itemID }, transaction: UnitOfWork.Transaction);
        }

        public IEnumerable<ItemStatistic> GetByTimestamp(DateTime timestamp)
        {
            return UnitOfWork.Connection.Query<ItemStatistic>("SELECT * FROM ItemStatistic WHERE Timestamp = @Timestamp", new { Timestamp = timestamp }, transaction: UnitOfWork.Transaction);
        }
    }
}
