using EdgeWorks.Models.Auctions;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.CommunityAPIs;
using EdgeWorks.Shared.Configurations.BlizzardAPIs.WorldOfWarcraft.GameDataAPIs;
using EdgeWorks.Shared.Services.Authentication;
using EdgeWorks.Shared.Services.Files;
using EdgeWorks.Tools.Extensions;
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

        public async Task Test()
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

                    var aucions = await _fileService.LoadFromStorage<AuctionData>(files.First(x => x.Key == key).Value.Name);
                    Console.WriteFormatted("Succesfully loaded auction data for {0}", Color.White, TypeDictionary.FormatInput(aucions.Realms.First().Name));
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Exiting filestorage");
                    break;
                }
            }
        }
    }
}
