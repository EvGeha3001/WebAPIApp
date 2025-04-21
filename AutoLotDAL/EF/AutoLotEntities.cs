using AutoLotDAL.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure.Interception;
using AutoLotDAL.Interception;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using AutoLotDAL.Models.Base;

namespace AutoLotDAL.EF
{
    public partial class AutoLotEntities : DbContext
    {
        //static readonly DatabaseLogger DatabaseLogger = new DatabaseLogger("sqllog.txt", true);
        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
            //var context = (this as IObjectContextAdapter).ObjectContext;
            //context.ObjectMaterialized += OnObjectMaterialized;
            //DbInterception.Add(new ConsoleWriteInterceptor());
            //DatabaseLogger.StartLogging();
            //DbInterception.Add(DatabaseLogger);
        }



        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        protected override void Dispose(bool disposing)
        {
            //DbInterception.Remove(DatabaseLogger);
            //DatabaseLogger.StopLogging();
            base.Dispose(disposing);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);
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
