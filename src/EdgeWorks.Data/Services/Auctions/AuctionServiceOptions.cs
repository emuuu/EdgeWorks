﻿using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Options;
using Microsoft.Extensions.Configuration;

namespace EdgeWorks.Data.Auctions
{
    [ConfigurationName("Dapper")]
    public class AuctionServiceOptions : DapperServiceOptions
    {
        public AuctionServiceOptions(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("auctionsDB");
            ConnectionFactory = new SqLiteConnectionFactory();
        }

        /// <summary>	Gets or sets the connection factory. </summary>
        /// <value>	The connection factory. </value>
        /// <remarks> Overridden to make this property visible as DeclaredProperty. </remarks>
        public override IConnectionFactory ConnectionFactory { get; set; }

        /// <summary>	Gets or sets the connection string. </summary>
        /// <value>	The connection string. </value>
        /// <remarks> Overridden to make this property visible as DeclaredProperty. </remarks>
        public override string ConnectionString { get; set; }
    }
}
