using System.Data.Entity;
using ParkingHouse.DB.Entities;

namespace ParkingHouse.DB.Concrete
{
    public class EfDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}