using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ParkingHouse.DB.Entities
{
    public class Car
    {
        [HiddenInput(DisplayValue = false)]
        public int CarID { get; set; }  
        [DisplayName("Length (default: 4,00)")]
        [Range(0.4, 4)]
        [Required]
        public decimal CarLength { get; set; }
        [DisplayName("Width (default: 2,00)")]
        [Range(0.4, 2)]
        [Required]
        public decimal CarWidth { get; set; }
        [DisplayName("Has Contract?")]
        public bool HasContract { get; set; }
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime EntryTime { get; set; }
    }
}