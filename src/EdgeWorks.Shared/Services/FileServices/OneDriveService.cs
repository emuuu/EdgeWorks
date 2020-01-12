using EdgeWorks.Data.System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EdgeWorks.Shared.Services.Files
{
    public class OneDriveService : IFileService
    {
        private readonly ILogger<OneDriveService> _logger;


        public OneDriveService( ILogger<OneDriveService> logger = null)
        {


            _logger = logger;
        }

        public async Task<FileSaveResponse> SaveToStorage(string fileName, object file, bool compress)
        {
            var result = new FileSaveResponse
            {
                Filename = fileName
            };

            if (_logger != null)
                _logger.LogInformation("OneDrive: Finished saving {0} {1} success", fileName, result.IsSuccess ? "with" : "without");

            return result;
        }

    }
}
