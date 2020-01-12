using System;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluentMigrator.Runner.VersionTableInfo;
using EdgeWorks.Data.Configurations.SqliteOptions;
using EdgeWorks.Data.Auctions;
using EdgeWorks.Data.System;

namespace EdgeWorks.Data
{
    public class AuctionDataService : SqLiteDapperDataService
    {
        /// <summary>	Constructor. </summary>
        /// <param name="options">	Options for controlling the operation. </param>
        public AuctionDataService(AuctionServiceOptions options) : base(options)
        {
            RegisterRepositories();
        }

        public AuctionUnitOfWork StartUnitOfWork()
        {
            return new AuctionUnitOfWork(this);
        }
        private void RegisterRepositories()
        {
            RegisterRepositoryProvider(new Func<IUnitOfWork, IAuctionFileRepository>(work =>
                                       new AuctionFileRepository(work)));
        }

        public override string Name => nameof(AuctionDataService);

        public override SqlType SqlType => SqlType.Mssql;

        public override IVersionTableMetaData MetaData => null;
    }
}
