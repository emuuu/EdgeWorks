using EdgeWorks.Models.Auctions;
using EdgeWorks.Models.Items;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.WorldOfWarcraft.GameDataAPIs;
using EdgeWorks.Shared.Helpers;
using EdgeWorks.Shared.Services.Authentication;
using EdgeWorks.Shared.Services.Files;
using EdgeWorks.Statistics;
using EdgeWorks.Tools.Configuration;
using EdgeWorks.Tools.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace EdgeWorks.Tools.Services
{
    public class AnalyseService
    {
        private readonly IFileService _fileService;
        private readonly ITokenService _tokenService;
        private readonly ItemAPI _itemApi;
        private readonly IEnumerable<KeyValuePair<int, Item>> _observedItems;

        public AnalyseService(IFileService fileService, ITokenService tokenService, ItemAPI itemApi, IOptions<ObservedItems> observedItems)
        {
            _fileService = fileService;
            _tokenService = tokenService;
            _itemApi = itemApi;

            var itemInit = InitObservedItems(observedItems.Value);
            itemInit.Wait();
            _observedItems = itemInit.Result;
        }

        public async Task GetItemStatistics()
        {
            await PrintItemList();
            do
            {
                Console.WriteLine("Select an item", "00CC66".ToColor());
                
                if (int.TryParse(Console.ReadLine(), out int key) && _observedItems.Any(x => x.Key == key))
                {
                    var item = await GetItem(_observedItems.First(x => x.Key == key).Value.Id);
                    var auctionfile = await GetAuctionFiles();
                    if (auctionfile != null)
                    {
                        var auctions = auctionfile.Auctions.Where(x => x.Item == item.Id);
                        var statisticItem = await GenerateStatisticItem(auctions);
                        await PrintStatistic(statisticItem);
                    }
                }
                else
                {
                    break;
                }
            } while (true);
        }

        private async Task<IEnumerable<KeyValuePair<int, Item>>> InitObservedItems(ObservedItems observedItems)
        {
            var i = 1;
            var items = new List<Item>();
            foreach (var item in observedItems.Items)
            {
                items.Add(await GetItem(item.Id));
            }
            return items.Select(x => new KeyValuePair<int, Item>(i++, x)).ToList();
        }

        private async Task PrintItemList()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("");
                Console.WriteLine("Observed items", "00CC66".ToColor());

                foreach (var item in _observedItems)
                {
                    Console.WriteFormatted("{0}", Color.White, TypeDictionary.FormatInput(item.Key));
                    Console.WriteLineFormatted(" - {0}", Color.White, TypeDictionary.FormatInput(item.Value.Name));
                }
                Console.WriteLine("");
            });
        }

        private async Task<Item> GetItem(int itemID)
        {
            try
            {
                var accessToken = await _tokenService.GetAccessToken();
                var response = await HttpRequestFactory.Get(accessToken, _itemApi.Item(itemID));
                return response.ContentAsType<Item>();
            }
            catch (Exception ex)
            {
                ex.PrintException();
                return null;
            }
        }

        private async Task<AuctionData> GetAuctionFiles()
        {
            var i = 1;
            var files = (await _fileService.GetStorage()).Select(x => new KeyValuePair<int, FileInfo>(i++, new FileInfo(x))).ToList();
            files.ForEach(x =>
            {
                Console.WriteFormatted("{0}", Color.White, TypeDictionary.FormatInput(x.Key));
                Console.WriteLineFormatted(" - {0}", Color.White, TypeDictionary.FormatInput(x.Value.Name));
            });
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("Select a file:", "00CC66".ToColor());
            if (int.TryParse(Console.ReadLine(), out int key) && files.Any(x=>x.Key == key))
            {
                if (!files.Any(x => x.Key == key))
                {
                    Console.WriteLine("Exiting filestorage");
                    return null;
                }
                Console.WriteLine("");
                Console.WriteLine("Started loading file from storage..");
                return await _fileService.LoadFromStorage<AuctionData>(files.First(x => x.Key == key).Value.Name);
            }
            Console.WriteLine("No such file found.. Exiting filestorage");
            Console.WriteLine("");
            return null;
        }

        private async Task<StatisticItem> GenerateStatisticItem(IEnumerable<Auction> auctions)
        {
            return await Task.Run(() =>
            {
                var sample = auctions.SelectMany(x => Enumerable.Range(1, x.Quantity).Select(y => x.Buyout / x.Quantity / 10000.0));
                return new StatisticItem(sample.ToList());
            });
        }

        private async Task PrintStatistic(StatisticItem statisticItem)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("");
                Console.WriteLineFormatted("Sum: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.Sum, 2)));
                Console.WriteLineFormatted("Count: {0}", Color.White, TypeDictionary.FormatInput(statisticItem.Count));
                Console.WriteLineFormatted("ArithmeticMean: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.ArithmeticMean, 2)));
                Console.WriteLineFormatted("Median: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.Median, 2)));
                Console.WriteLineFormatted("Minimum: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.Minimum, 2)));
                Console.WriteLineFormatted("Maxmimum: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.Maxmimum, 2)));
                Console.WriteLineFormatted("Range: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.Range, 2)));
                Console.WriteLineFormatted("Variance: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.Variance, 2)));
                Console.WriteLineFormatted("StandardDeviation: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.StandardDeviation, 2)));
                Console.WriteLineFormatted("SampleVariance: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.SampleVariance, 2)));
                Console.WriteLineFormatted("SampleStandardDeviation: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.SampleStandardDeviation, 2)));
                Console.WriteLineFormatted("LowerQuartile: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.LowerQuartile, 2)));
                Console.WriteLineFormatted("HigherQuartile: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.HigherQuartile, 2)));
                Console.WriteLineFormatted("QuartileDistance: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statisticItem.QuartileDistance, 2)));
                Console.WriteLine("");
            });
        }
    }
}
