using System.Linq;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Entities;

namespace ParkingHouse.DB.Concrete
{
    public class EfCurrentProfitRepository : ISummaryRepository
    {
        private readonly EfDbContext context = new EfDbContext();

        public IQueryable<CurrentProfit> CurrentProfits
        {
            get { return context.CurrentProfits; }
        }

        public void SaveSum(double sum, int totalCars)
        {
            var profit = new CurrentProfit { CurrentSum = (decimal) sum, TotalCars = totalCars};           
            context.CurrentProfits.Add(profit);
            context.SaveChanges();
        }

        public void RemoveAll()
        {
            foreach (var temporaryProfit in context.CurrentProfits)
            {
                context.CurrentProfits.Remove(temporaryProfit);
            }
            context.SaveChanges();

        }
    }
}