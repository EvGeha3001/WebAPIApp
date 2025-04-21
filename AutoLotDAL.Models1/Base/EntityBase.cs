using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace AutoLotDAL.Models.Base
{
    public class EntityBase : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        [NotMapped]
        public bool IsChanged { get; set; }

        public virtual event PropertyChangedEventHandler PropertyChanged;       
    }
}
