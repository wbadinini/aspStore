namespace UPFStore
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using UPFStore.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class UPFStoreModel : DbContext
    {
        public UPFStoreModel()
            : base("name=UPFStoreModel")
        {
        }

        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OfferImage> OfferImages { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
