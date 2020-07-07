using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPFStore.Models
{
    public class OfferImage
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public Offer Offer { get; set; }
    }
}