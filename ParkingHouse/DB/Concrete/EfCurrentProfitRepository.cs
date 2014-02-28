using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Entities;
using ParkingHouse.Models;

namespace ParkingHouse.DB.Concrete
{
    public class EfCurrentProfitRepository : ISummaryRepository
    {
        private EfDbContext context = new EfDbContext();

        public IQueryable<CurrentProfit> CurrentProfits
        {
            get { return context.CurrentProfits; }
        }

        public void SaveSum(double sum, int totalCars)
        {
            CurrentProfit profit = new CurrentProfit { CurrentSum = (decimal) sum, TotalCars = totalCars};           
            context.CurrentProfits.Add(profit);
            context.SaveChanges();
        }

        public void RemoveAll()
        {
            foreach (CurrentProfit temporaryProfit in context.CurrentProfits)
            {
                context.CurrentProfits.Remove(temporaryProfit);
            }
            context.SaveChanges();

        }
    }
}