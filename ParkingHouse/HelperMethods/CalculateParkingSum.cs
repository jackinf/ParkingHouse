using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParkingHouse.DB.Entities;

namespace ParkingHouse.HelperMethods
{
    public static class CalculateParkingSum
    {
        private static double _sum;
        private const double  HourlyFee = 0.50;
        private const double ContractFee = 40.00;
        public static double GetCarParkingSum(Car car)
        {
            if (car.HasContract)
            {
                _sum = ContractFee;
            }
            else
            {
                TimeSpan elapsedTime = car.EntryTime - DateTime.Now;
                
                _sum = (elapsedTime.Hours+1)*HourlyFee;
            }
            return _sum;
        }
    }
}