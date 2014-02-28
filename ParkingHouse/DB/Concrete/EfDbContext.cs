using System.Data.Entity;
using ParkingHouse.DB.Entities;
using ParkingHouse.Models;

namespace ParkingHouse.DB.Concrete
{
    public class EfDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CurrentProfit> CurrentProfits { get; set; }
    }
}