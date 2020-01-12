using EdgeWorks.Data.System;
using System.Threading.Tasks;

namespace EdgeWorks.Shared.Services.Files
{
    public interface IFileService
    {
        Task<FileSaveResponse> SaveToStorage(string fileName, object file, bool compress);
    }
}
