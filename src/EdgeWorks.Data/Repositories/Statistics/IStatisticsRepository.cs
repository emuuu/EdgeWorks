using FluiTec.AppFx.Data;
using System.Collections.Generic;

namespace EdgeWorks.Data.Statistics
{
    public interface IStatisticsRepository : IDataRepository<ItemStatistic, int>
    {
        IEnumerable<ItemStatistic> GetByItem(long itemId);
    }
}
