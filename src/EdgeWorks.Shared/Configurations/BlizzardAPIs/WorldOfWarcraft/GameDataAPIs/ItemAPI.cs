using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdgeWorks.Shared.Configurations.BlizzardAPIs.WorldOfWarcraft.GameDataAPIs
{
    public class ItemAPI
    {
        private ApiSettings _settings;

        public ItemAPI(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        public string ItemClassesIndex
        {
            get
            {
                return string.Format("https://{0}.api.blizzard.com/data/wow/item-class/index?namespace={1}&locale={2}", _settings.Region, _settings.Static, _settings.Locale);
            }
        }

        public string ItemClass(int itemClassId)
        {
            return string.Format("https://{0}.api.blizzard.com/data/wow/item-class/{1}?namespace={2}&locale={3}", _settings.Region, itemClassId, _settings.Static, _settings.Locale);
        }

        public string ItemSubClass(int itemClassId, int itemSubclassId)
        {
            return string.Format("https://{0}.api.blizzard.com/data/wow/item-class/{1}/item-subclass/{2}?namespace={3}&locale={4}", _settings.Region, itemClassId, itemSubclassId, _settings.Static, _settings.Locale);
        }

        public string Item(int itemId)
        {
            return string.Format("https://{0}.api.blizzard.com/data/wow/item/{1}?namespace={2}&locale={3}", _settings.Region, itemId, _settings.Static, _settings.Locale);
        }

        public string ItemMedia(int itemId)
        {
            return string.Format("https://{0}.api.blizzard.com/data/wow/media/item/{1}?namespace={2}&locale={3}", _settings.Region, itemId, _settings.Static, _settings.Locale);
        }
    }
}
