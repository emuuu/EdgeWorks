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
        }
    }
}
