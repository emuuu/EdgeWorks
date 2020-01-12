using EdgeWorks.Data;
using EdgeWorks.Data.System;
using EdgeWorks.Shared.Configuration;
using EdgeWorks.Shared.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace EdgeWorks.Shared.Services.Files
{
    public class LocalFileService : IFileService
    {
        private readonly FileDataService _dataService;
        private readonly ILogger<LocalFileService> _logger;
        private readonly string _fileStorage;

        public LocalFileService(FileDataService dataService, IOptions<ServerPaths> pathOptions, ILogger<LocalFileService> logger)
        {
            _dataService = dataService;
            _fileStorage = pathOptions.Value.FileStorage;
            if (!Directory.Exists(_fileStorage))
            {
                Directory.CreateDirectory(_fileStorage);
            }

            _logger = logger;
        }

        public async Task<FileSaveResponse> SaveToStorage(string fileName, object file, bool compress)
        {
            using (var uow = _dataService.StartUnitOfWork())
            {
                var fileResponse = new FileSaveResponse
                {
                    Filename = fileName
                };
                try
                {
                    var fileExtension = compress ? ".zip" : ".json";
                    var filePath = _fileStorage + fileName + fileExtension;
                    if (!File.Exists(filePath))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            if (compress)
                            {
                                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                                {
                                    var auctionFile = archive.CreateEntry(fileName + ".json");

                                    using (var entryStream = auctionFile.Open())
                                    using (var streamWriter = new StreamWriter(entryStream))
                                    {
                                        await streamWriter.WriteAsync(file.ToJson());
                                    }
                                }
                            }
                            else
                            {
                                using (var streamWriter = new StreamWriter(memoryStream))
                                {
                                    await streamWriter.WriteAsync(file.ToJson());
                                }
                            }
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                memoryStream.Seek(0, SeekOrigin.Begin);
                                memoryStream.CopyTo(fileStream);
                            }
                        }
                    }
                    var fileInfo = new FileInfo(filePath);
                    fileResponse.IsSuccess = true;
                    fileResponse.Hash = fileInfo.CalculateMD5();
                    fileResponse.Path = filePath;
                    fileResponse.Extension = fileExtension;
                }
                catch (Exception ex)
                {
                    fileResponse.IsSuccess = false;
                    fileResponse.ErrorMessage = ex.Message;

                    if (_logger != null)
                        _logger.LogError(ex, "Failed to save file {0}", fileName);
                }

                if (_logger != null)
                    _logger.LogInformation("LocalFile: Finished saving {0} {1} success", fileName, fileResponse.IsSuccess ? "with" : "without");

                var result = uow.FileSaveResponseRepository.Add(fileResponse);
                uow.Commit();

                return result;
            }
        }
    }
}
