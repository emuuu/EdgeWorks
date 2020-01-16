using EdgeWorks.Data.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdgeWorks.Shared.Services.Files
{
    public interface IFileService
    {
        Task<FileSaveResponse> SaveToStorage(string subStorage, string fileName, object file, bool compress);
        
        Task<IEnumerable<string>> GetStorage(string subStorage);

        Task<T> LoadFromStorage<T>(string subStorage, string fileName);
    }
}
