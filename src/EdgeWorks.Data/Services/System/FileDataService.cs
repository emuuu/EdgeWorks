using System;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluentMigrator.Runner.VersionTableInfo;

namespace EdgeWorks.Data.System
{
    public class FileDataService : SqLiteDapperDataService
    {
        /// <summary>	Constructor. </summary>
        /// <param name="options">	Options for controlling the operation. </param>
        public FileDataService(FileServiceOptions options) : base(options)
        {
            RegisterRepositories();
        }

        public FileUnitOfWork StartUnitOfWork()
        {
            return new FileUnitOfWork(this);
        }
        private void RegisterRepositories()
        {
            RegisterRepositoryProvider(new Func<IUnitOfWork, IFileSaveResponseRepository>(work =>
                           new FileSaveResponseRepository(work)));

        }

        public override string Name => nameof(FileDataService);

        public override SqlType SqlType => SqlType.Mssql;

        public override IVersionTableMetaData MetaData => null;
    }
}
