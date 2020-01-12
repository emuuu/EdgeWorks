using AutoMapper;

namespace EdgeWorks.AuctionHouse.Scraper.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Auctions.AuctionFile, Data.Auctions.AuctionFile>();
        }
    }
}
