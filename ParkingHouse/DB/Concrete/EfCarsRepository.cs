using System;
using System.Linq;
using System.Web.Mvc;
using ParkingHouse.Controllers;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Entities;
using ParkingHouse.HelperMethods;

namespace ParkingHouse.DB.Concrete
{
    public class EfCarsRepository : ICarsParkingRepository
    {
        private EfDbContext context = new EfDbContext();
        private const int ParkingSpaces = 500;

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
        
        public void CheckForErrors(Car car, ParkingController parkingController)
        {
            int carsWithContract = (from cars in context.Cars where cars.HasContract select cars).Count();
            int carsInParkingLot = context.Cars.Count();
            if (carsInParkingLot < ParkingSpaces)
            {
                if (carsInParkingLot >= (ParkingSpaces - ParkingSpaces*0.1) && carsWithContract < ParkingSpaces*0.1 &&
                    car.HasContract)
                {
                    context.Cars.Add(car);
                }else if (carsInParkingLot >= (ParkingSpaces - ParkingSpaces*0.1) && carsWithContract < ParkingSpaces*0.1 &&
                          !car.HasContract)
                {
                    int reservedSpaces = (int)(ParkingSpaces * 0.1) - carsWithContract;
                    if (reservedSpaces < 0) reservedSpaces = 0;
                    int freeSpaces = ParkingSpaces - (carsWithContract + reservedSpaces + (int)(ParkingSpaces - ParkingSpaces * 0.1));
                    if (freeSpaces > 0) context.Cars.Add(car);                    
                    else
                    {
                        parkingController.ModelState.AddModelError("error",
                                string.Format("Last {0} spot(s) reserved for clients with a contract. Come back later.", reservedSpaces));
                    }
                }                                           
            }
            else
            {
                parkingController.ModelState.AddModelError("error",
                            "The parking house is full. Come back later.");
            }
        }
    }
}