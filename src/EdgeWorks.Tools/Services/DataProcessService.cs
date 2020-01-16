using EdgeWorks.Models.Auctions;
using EdgeWorks.Models.Items;
using EdgeWorks.Shared.Services.Files;
using EdgeWorks.Tools.Configuration;
using EdgeWorks.Tools.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace EdgeWorks.Tools.Services
{
    public class DataProcessService
    {
        private readonly IFileService _fileService;
        private readonly IEnumerable<Item> _observedItems;

        public DataProcessService(IFileService fileService, IOptions<ObservedItems> observedItems)
        {
            _fileService = fileService;
            _observedItems = observedItems.Value.Items;
        }


        public async Task FilterAuctionFiles()
        {
            try
            {
                Console.WriteLine("Start loading files from RawData..");
                Console.WriteLine("");

                var files = await _fileService.GetStorage("RawData");

                Console.WriteLine("Finished loading files from RawData");

                Console.WriteLine("Start filtering auction data for observed items..");

                var tasks = files.Select(x => ProcessAuctionFile(new FileInfo(x).Name)).ToArray();
                Task.WaitAll(tasks);
            }
            catch (Exception ex)
            {
                ex.PrintException();
            }
        }

        private async Task ProcessAuctionFile(string fileName)
        {
            try
            {
                var auctionData = await _fileService.LoadFromStorage<AuctionData>("RawData", fileName);
                auctionData.Auctions = auctionData.Auctions.Where(x => _observedItems.Select(y => y.Id).Contains(x.Item));
                await _fileService.SaveToStorage("Filtered", fileName.Split('.')[0], auctionData, false);

                Console.WriteLine("Finished processing {0}", fileName);
            }
            catch (Exception)
            {
                Console.WriteLineFormatted("File corrupted  {0}", Color.White, TypeDictionary.FormatInput(fileName));
            }
        }

    }
}
