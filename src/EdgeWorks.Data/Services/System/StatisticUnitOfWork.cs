using FluiTec.AppFx.Data.Dapper;

namespace EdgeWorks.Data.Statistics
{
    public class StatisticUnitOfWork : DapperUnitOfWork
    {
        /// <summary>	Constructor. </summary>
        /// <param name="dataService">	The data service. </param>
        public StatisticUnitOfWork(DapperDataService dataService) : base(dataService)
        {
        }

        private IStatisticsRepository _statisticsRepository;

        public IStatisticsRepository StatisticsRepository => _statisticsRepository ??
                                                       (_statisticsRepository =
                                                           GetRepository<IStatisticsRepository>());
    }
}
