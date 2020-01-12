using AutoMapper;
using EdgeWorks.Shared.Configurations.EdgeWorksAPIs.CommunityAPIs;
using EdgeWorks.Data;
using EdgeWorks.Shared.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using EdgeWorks.Shared.Configurations.BlizzardAPIs;
using EdgeWorks.Shared.Services.Authentication;
using EdgeWorks.Shared.Services.Files;

namespace EdgeWorks.AuctionHouse.Scraper.Services
{
    public class ScrapeService
    {
        private readonly IFileService _fileService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<ScrapeService> _logger;
        private readonly AuctionAPI _auctionApi;
        private readonly AuctionDataService _dataService;
        private readonly IMapper _mapper;

        public ScrapeService(IFileService fileService, IMapper mapper, AuctionDataService dataService, ITokenService tokenService, IOptions<ApiSettings> _apiSettings, ILogger<ScrapeService> logger)
        {
            _dataService = dataService;
            _fileService = fileService;
            _tokenService = tokenService;
            _mapper = mapper;
            _auctionApi = new AuctionAPI(_apiSettings.Value.Region, _apiSettings.Value.Realm, _apiSettings.Value.Locale);

            _logger = logger;
        }

        public async Task GetAuctions()
        {
            
            using (var uow = _dataService.StartUnitOfWork())
            {
                var accessToken = await _tokenService.GetAccessToken();

                _logger.LogTrace("Start reading latest auction file");
                var response = await HttpRequestFactory.Get(accessToken, _auctionApi.RequestUrl);
                var auctionFiles = response.ContentAsType<Models.Auctions.AuctionFiles>();

                foreach (var auctionFile in auctionFiles.Files)
                {
                    _logger.LogTrace("Processing file: {0}", auctionFile.LastModified);
                    
                    var existingFile = await uow.AuctionFileRepository.GetByLastModified(auctionFile.LastModified);
                    if (existingFile == null)
                    {
                        _logger.LogInformation("Start downloading file: {0}", auctionFile.LastModified);

                        try
                        {
                            response = await HttpRequestFactory.Get(accessToken, auctionFile.Url);
                            var auctions = response.ContentAsType<Models.Auctions.AuctionData>();

                            var fileResonse = await _fileService.SaveToStorage(auctionFile.LastModified.ToString(), auctions, true);

                            if (fileResonse.IsSuccess)
                            {
                                var mappedAuctionFile = _mapper.Map<Data.Auctions.AuctionFile>(auctionFile);
                                uow.AuctionFileRepository.Add(mappedAuctionFile);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to download file: {0}", auctionFile.LastModified);
                        }
                        uow.Commit();
                    }
                    else
                    {
                        _logger.LogTrace("File already exists: {0}", existingFile.LastModified);
                    }
                }
            }
        }
    }
}
