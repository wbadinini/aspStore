using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPFStore.Models
{
    public class Offer
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<OfferImage> OfferImages { get; set; }
    }
}