using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Dapper;
using System.Threading.Tasks;

namespace EdgeWorks.Data.System
{
    public class FileSaveResponseRepository : DapperRepository<FileSaveResponse, int>, IFileSaveResponseRepository
    {
        public FileSaveResponseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            UnitOfWork.Connection.Execute("CREATE TABLE IF NOT EXISTS 'FileSaveResponse' ( 'Id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 'Filename' TEXT NOT NULL, 'Extension' TEXT, 'Hash' TEXT, 'Path' TEXT, 'IsSuccess' INTEGER NOT NULL, 'ErrorMessage' TEXT )", transaction: UnitOfWork.Transaction);
        }
    }
}
