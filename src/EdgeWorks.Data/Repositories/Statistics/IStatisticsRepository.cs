using FluiTec.AppFx.Data;
using System;
using System.Collections.Generic;

namespace EdgeWorks.Data.Statistics
{
    public interface IStatisticsRepository : IDataRepository<ItemStatistic, int>
    {
        IEnumerable<ItemStatistic> GetByItem(long itemId);

        IEnumerable<ItemStatistic> GetByTimestamp(DateTime timestamp);
    }
}
