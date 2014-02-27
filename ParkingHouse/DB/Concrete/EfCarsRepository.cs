using System;
using System.Linq;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Entities;
using ParkingHouse.HelperMethods;

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
            car.EntryTime = DateTime.Now;
            context.Cars.Add(car);            
            context.SaveChanges();
        }

        public double RemoveCar(int carId)
        {
            Car car = context.Cars.FirstOrDefault(p => p.CarID == carId);
            double sum = CalculateParkingSum.GetCarParkingSum(car);
            context.Cars.Remove(car);
            context.SaveChanges();
            return sum;
        }
    }
}