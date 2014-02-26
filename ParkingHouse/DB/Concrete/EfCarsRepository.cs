using System.Linq;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Entities;

namespace ParkingHouse.DB.Concrete
{
    public class EfCarsRepository : ICarsParkingRepository
    {
        private EfDbContext context = new EfDbContext();

        public IQueryable<Car> Cars
        {
            get { return context.Cars; }
        }
        public void AddCar(Car car)
        {
            context.Cars.Add(car);            
            context.SaveChanges();
        }

        public void RemoveCar(int carId)
        {
            Car car = context.Cars.FirstOrDefault(p => p.CarID == carId);
            context.Cars.Remove(car);
            context.SaveChanges();
        }
    }
}