using EdgeWorks.Data.Statistics;
using EdgeWorks.Models.Auctions;
using EdgeWorks.Models.Items;
using EdgeWorks.Shared.Services.Files;
using EdgeWorks.Statistics;
using EdgeWorks.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EdgeWorks.Tools.Services
{
    public class DataProcessService
    {
        private readonly StatisticDataService _statisticService;
        private readonly IFileService _fileService;
        private readonly IEnumerable<Item> _observedItems;
        ILogger<DataProcessService> _logger;

        public DataProcessService(StatisticDataService statisticService, IFileService fileService, IOptions<ObservedItems> observedItems, ILogger<DataProcessService> logger)
        {
            _statisticService = statisticService;
            _fileService = fileService;
            _observedItems = observedItems.Value.Items;
            _logger = logger;
        }

        public async Task FilterAuctionFiles()
        {
            try
            {
                var rawDataStorage = "RawData";
                var targetStorage = "Filtered";

                _logger.LogInformation("Started loading files from {0}", rawDataStorage);

                var files = await _fileService.GetStorage(rawDataStorage);
                var filtered = await _fileService.GetStorage(targetStorage);
                var filteredFiles = new List<FileInfo>();
                filteredFiles.AddRange(files.Where(x => !filtered.Select(y => Path.GetFileNameWithoutExtension(y.Name)).Any(y => y == Path.GetFileNameWithoutExtension(x.Name))));

                _logger.LogInformation("Finished loading files from {0}", rawDataStorage);

                _logger.LogInformation("Started filtering auction data for observed items");

                var i = 0;
                var fileCount = filteredFiles.Count();
                while (i < fileCount)
                {
                    var take = 5;
                    if (i + take >= fileCount)
                    {
                        take = fileCount - i;
                    }
                    var tasks = filteredFiles.Skip(i).Take(take).Select(x => ProcessAuctionFile(rawDataStorage, x.Name, targetStorage)).ToArray();
                    
                    Task.WaitAll(tasks);
                    i += take;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occued during filtering raw data");
            }
        }

        public async Task CreateItemStatistics(string subStorage)
        {
            try
            {
                var files = await _fileService.GetStorage(subStorage);

                using (var uow = _statisticService.StartUnitOfWork())
                {
                    foreach (var file in files)
                    {
                        if (!long.TryParse(file.Name.Split('.')[0], out long epoch))
                        {
                            _logger.LogError("{0} file not properly name as epoch", file.Name);
                            break;
                        }
                        var timeStamp = new DateTime(1970, 1, 1).AddMilliseconds(epoch);

                        var existingData = uow.StatisticsRepository.GetByTimestamp(timeStamp);

                        if (!existingData.Any())
                        {
                            var auctionData = await _fileService.LoadFromStorage<AuctionData>(subStorage, file.Name);
                            if (auctionData != default)
                            {
                                foreach (var item in _observedItems)
                                {
                                    if (auctionData.Auctions.Any(x => x.Item == item.Id))
                                    {
                                        var statisticItem = new StatisticItem(auctionData.Auctions.Where(x => x.Item == item.Id).SelectMany(x => Enumerable.Range(1, x.Quantity).Select(y => x.Buyout / x.Quantity / 10000.0)).ToList());

                                        uow.StatisticsRepository.Add(new ItemStatistic
                                        {
                                            ItemID = item.Id,
                                            Name = item.Name,
                                            TimeStamp = timeStamp,
                                            Sum = statisticItem.Sum,
                                            Count = statisticItem.Count,
                                            ArithmeticMean = statisticItem.ArithmeticMean,
                                            Median = statisticItem.Median,
                                            Minimum = statisticItem.Minimum,
                                            Maxmimum = statisticItem.Maxmimum,
                                            Range = statisticItem.Range,
                                            Variance = statisticItem.Variance,
                                            StandardDeviation = statisticItem.StandardDeviation,
                                            SampleVariance = statisticItem.SampleVariance,
                                            SampleStandardDeviation = statisticItem.SampleStandardDeviation,
                                            LowerQuartile = statisticItem.LowerQuartile,
                                            HigherQuartile = statisticItem.HigherQuartile,
                                            QuartileDistance = statisticItem.QuartileDistance
                                        });

                                        _logger.LogInformation("Finished processing {0} for {1}", item.Name, timeStamp);
                                    }
                                }
                            }
                        }
                    }
                    uow.Commit();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create item statistic for storage {0}", subStorage);
            }
        }

        private async Task ProcessAuctionFile(string rawDataStorage, string fileName, string targetStorage)
        {
            var auctionData = await _fileService.LoadFromStorage<AuctionData>(rawDataStorage, fileName);
            if (auctionData != default)
            {
                using (var uow = _statisticService.StartUnitOfWork())
                {
                    auctionData.Auctions = auctionData.Auctions.Where(x => _observedItems.Select(y => y.Id).Contains(x.Item));
                    await _fileService.SaveToStorage(targetStorage, fileName.Split('.')[0], auctionData, false);

                    _logger.LogInformation("Finished processing {0}", fileName);
                }
            }
        }

    }
}
