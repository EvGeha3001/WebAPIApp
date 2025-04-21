using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using AutoLotDAL.Models.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using AutoLotDAL.Models.MetaData;

namespace AutoLotDAL.Models
{
    [MetadataType(typeof(InventoryMetaData))]
    public partial class Inventory : EntityBase
    {
        public override string ToString()
        {
            return $"{this.PetName ?? "Unknown"} is a {this.Color ?? "Unknown"} " +
                $"{this.Make ?? "Unknown"} with ID {this.Id}";
        }
        private int _carId;
        private string _make;
        private string _color;
        private string _petName;
        private bool _isChanged = false;

        public override event PropertyChangedEventHandler PropertyChanged;
                
        [Required]
        [StringLength(50)]
        public string Make
        {
            get => _make;
            set
            {
                if (value == _make) return;
                _make = value;
                //OnPropertyChanged();
            }
        }
        [Required]
        [StringLength(50)]
        public string Color
        {
            get => _color;
            set
            {
                if (value == _color) return;
                _color = value;
                //OnPropertyChanged();
            }
        }
        [StringLength(50)]
        public string PetName
        {
            get => _petName;
            set
            {
                if (value == _petName) return;
                _petName = value;
                //OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
