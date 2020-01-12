using EdgeWorks.Data.Auctions;
using EdgeWorks.Data.System;
using FluiTec.AppFx.Data.Dapper;

namespace EdgeWorks.Data
{
    public class AuctionUnitOfWork : DapperUnitOfWork
    {
        /// <summary>	Constructor. </summary>
        /// <param name="dataService">	The data service. </param>
        public AuctionUnitOfWork(DapperDataService dataService) : base(dataService)
        {
        }

        private IAuctionFileRepository _auctionFileRepository;

        public IAuctionFileRepository AuctionFileRepository => _auctionFileRepository ??
                                                       (_auctionFileRepository =
                                                           GetRepository<IAuctionFileRepository>());
    }
}
