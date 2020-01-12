using System.Collections.Generic;

namespace EdgeWorks.Models.Auctions
{
    public class Auction
    {
        public long Auc { get; set; }

        public long Item { get; set; }

        public string OwnerRealm { get; set; }

        public long Bid { get; set; }

        public long Buyout { get; set; }

        public int Quantity { get; set; }

        public string TimeLeft { get; set; }

        public int Rand { get; set; }

        public int Seed { get; set; }

        public int Context { get; set; }

        public IEnumerable<Modifier> Modifiers { get; set; }

        public int PetSpeciesId { get; set; }

        public int PetBreedId { get; set; }

        public int PetLevel { get; set; }

        public int PetQualityId { get; set; }
    }
}
