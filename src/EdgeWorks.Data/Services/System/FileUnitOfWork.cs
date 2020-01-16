using FluiTec.AppFx.Data.Dapper;

namespace EdgeWorks.Data.System
{
    public class FileUnitOfWork : DapperUnitOfWork
    {
        /// <summary>	Constructor. </summary>
        /// <param name="dataService">	The data service. </param>
        public FileUnitOfWork(DapperDataService dataService) : base(dataService)
        {
        }

        private IFileSaveResponseRepository _fileSaveResponseRepository;

        public IFileSaveResponseRepository FileSaveResponseRepository => _fileSaveResponseRepository ??
                                                       (_fileSaveResponseRepository =
                                                           GetRepository<IFileSaveResponseRepository>());
    }
}
