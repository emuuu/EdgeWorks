using EdgeWorks.Models.Auctions;
using EdgeWorks.Models.Items;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.WorldOfWarcraft.GameDataAPIs;
using EdgeWorks.Shared.Helpers;
using EdgeWorks.Shared.Services.Authentication;
using EdgeWorks.Shared.Services.Files;
using EdgeWorks.Statistics.Statistics;
using EdgeWorks.Tools.Extensions;
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

        public AnalyseService(IFileService fileService, ITokenService tokenService, ItemAPI itemApi)
        {
            _fileService = fileService;
            _tokenService = tokenService;
            _itemApi = itemApi;
        }

        public async Task GetItemStatistic(int itemID)
        {
            Console.WriteLine("Select sample file:");
            var auctionData = await GetAuctionFile();
            var item = await GetItem(itemID);
            var auctions = auctionData.Auctions.Where(x => x.Item == itemID);
            //var statistic = new StatisticItem(auctions.SelectMany(x => Enumerable.Repeat(x, x.Quantity).Select(y => y.Buyout / 10000.0)).ToList());
            var statistic = new StatisticItem(auctions.Select(y => y.Buyout / 10000.0).ToList());

            Console.WriteLine("");
            Console.WriteLine("{0}", item.Name, "00CC66".ToColor());
            Console.WriteLineFormatted("Sum: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.Sum, 2)));
            Console.WriteLineFormatted("Count: {0}", Color.White, TypeDictionary.FormatInput(statistic.Count));
            Console.WriteLineFormatted("ArithmeticMean: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.ArithmeticMean, 2)));
            Console.WriteLineFormatted("Median: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.Median, 2)));
            Console.WriteLineFormatted("Minimum: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.Minimum, 2)));
            Console.WriteLineFormatted("Maxmimum: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.Maxmimum, 2)));
            Console.WriteLineFormatted("Range: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.Range, 2)));
            Console.WriteLineFormatted("Variance: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.Variance, 2)));
            Console.WriteLineFormatted("StandardDeviation: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.StandardDeviation, 2)));
            Console.WriteLineFormatted("SampleVariance: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.SampleVariance, 2)));
            Console.WriteLineFormatted("SampleStandardDeviation: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.SampleStandardDeviation, 2)));
            Console.WriteLineFormatted("LowerQuartile: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.LowerQuartile, 2)));
            Console.WriteLineFormatted("HigherQuartile: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.HigherQuartile, 2)));
            Console.WriteLineFormatted("QuartileDistance: {0}", Color.White, TypeDictionary.FormatInput(Math.Round(statistic.QuartileDistance, 2)));

        }

        public async Task<AuctionData> GetAuctionFile()
        {
            var i = 1;
            var files = (await _fileService.GetStorage()).Select(x => new KeyValuePair<int, FileInfo>(i++, new FileInfo(x))).ToList();
            files.ForEach(x =>
            {
                Console.WriteFormatted("{0}", Color.White, TypeDictionary.FormatInput(x.Key));
                Console.WriteLineFormatted(" - {0}", Color.White, TypeDictionary.FormatInput(x.Value.Name));
            });

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Select a file:", "00CC66".ToColor());
                if (int.TryParse(Console.ReadLine(), out int key))
                {
                    if (!files.Any(x => x.Key == key))
                    {
                        Console.WriteLine("Exiting filestorage");
                        break;
                    }
                    Console.WriteLine("");
                    return await _fileService.LoadFromStorage<AuctionData>(files.First(x => x.Key == key).Value.Name);
                }
                else
                {
                    Console.WriteLine("Exiting filestorage");
                    Console.WriteLine("");
                    break;
                }
            }
            return null;
        }

        public async Task<Item> GetItem(int itemID)
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
    }
}
