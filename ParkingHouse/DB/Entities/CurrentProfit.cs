using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingHouse.DB.Entities
{
    public class CurrentProfit
    {
        public int ID {get;set; }
        public decimal CurrentSum { get;set; }
        public int TotalCars { get; set; }
    }
}