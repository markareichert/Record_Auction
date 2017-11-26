using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Record_Auction.Models.RecordModels
{
    public class Auction
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal MinBid { get; set; }

        //public IList<Record_Auction> Record_Auctions { get; set; }
    }
}
