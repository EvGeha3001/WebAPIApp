using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoLotDALNEW.Models;
using AutoLotDALNEW.Models.Base;

namespace AutoLotDALNEW.EF
{
    public partial class AutoLotEntities : DbContext
    {
        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
            var context = (this as IObjectContextAdapter).ObjectContext;
            context.ObjectMaterialized += OnObjectMaterialized;
        }

        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditRisk>()
                .Property(e => e.Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Inventory>()
                .Property(e => e.Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Inventory>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Car)
                .HasForeignKey(e => e.CarId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Timestamp)
                .IsFixedLength();
        }
        private void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            var model = (e.Entity as EntityBase);
            if (model != null)
            {
                model.IsChanged = false;
            }
        }
    }
}
