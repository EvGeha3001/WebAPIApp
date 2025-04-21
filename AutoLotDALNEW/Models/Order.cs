namespace AutoLotDALNEW.Models
{
    using AutoLotDALNEW.Models.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order : EntityBase
    {
        public int CustomerId { get; set; }

        public int CarId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Inventory Car { get; set; }
    }
}
