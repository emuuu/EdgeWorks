using System;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluentMigrator.Runner.VersionTableInfo;

namespace EdgeWorks.Data.Statistics
{
    public class StatisticDataService : SqLiteDapperDataService
    {
        /// <summary>	Constructor. </summary>
        /// <param name="options">	Options for controlling the operation. </param>
        public StatisticDataService(StatisticServiceOptions options) : base(options)
        {
            RegisterRepositories();
        }

        public StatisticUnitOfWork StartUnitOfWork()
        {
            return new StatisticUnitOfWork(this);
        }
        private void RegisterRepositories()
        {
            RegisterRepositoryProvider(new Func<IUnitOfWork, IStatisticsRepository>(work =>
                           new StatisticsRepository(work)));

        }

        public override string Name => nameof(StatisticDataService);

        public override SqlType SqlType => SqlType.Mssql;

        public override IVersionTableMetaData MetaData => null;
    }
}
