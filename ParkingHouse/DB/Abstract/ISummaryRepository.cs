using System.Linq;
using ParkingHouse.DB.Entities;

namespace ParkingHouse.DB.Abstract
{
    public interface ISummaryRepository
    {
        IQueryable<CurrentProfit> CurrentProfits { get; }
        void SaveSum(double sum, int totalCars);
        void RemoveAll();
    }
}
