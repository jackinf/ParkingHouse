using System;
using System.Linq;
using ParkingHouse.Controllers;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Entities;
using ParkingHouse.HelperMethods;

namespace ParkingHouse.DB.Concrete
{
    public class EfCarsRepository : ICarsParkingRepository
    {
        private readonly EfDbContext context = new EfDbContext();
        private const int TotalParkingSpaces = 500;
        private const int TotalFreeParkingSpaces = (int)(TotalParkingSpaces * 0.9);
        private const int TotalContractParingSpaces = (int)(TotalParkingSpaces * 0.1);

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
            var car = context.Cars.FirstOrDefault(p => p.CarID == carId);
            var sum = CalculateParkingSum.GetCarParkingSum(car);
            context.Cars.Remove(car);
            context.SaveChanges();
            return sum;
        }

        /// <summary>
        /// Adds cars into parking spaces according if car has contract or not.
        /// </summary>
        /// <param name="car">Car which is being added</param>
        /// <param name="parkingController">Used to set modelstate to false if any errors occur</param>
        public void AddCarToParkingLot(Car car, ParkingController parkingController)
        {
            var carsInParkingLotWithContract = context.Cars.Count(c => c.HasContract);
            var carsInParkingLot = context.Cars.Count();

            if (!HasFreeParkingSpaces(carsInParkingLot))
            {
                parkingController.ModelState.AddModelError("error",
                    "The parking house is full. Come back later.");
                return;
            }

            // try to add a car with contract
            if (CanAddCarsWhenNoFreeSpaceLeft(carsInParkingLot, carsInParkingLotWithContract, car.HasContract))
            {
                context.Cars.Add(car);
                return;
            }

            // try to add a car with no contract
            if (CanAddCarsWhenNoFreeSpaceLeft(carsInParkingLot, carsInParkingLotWithContract, !car.HasContract))
            {
                var reservedSpaces = GetReservedSpaces(carsInParkingLotWithContract);
                if (GetFreeSpaces(carsInParkingLotWithContract, reservedSpaces) <= 0)
                {
                    parkingController.ModelState.AddModelError("error",
                        string.Format(
                            "Last {0} spot(s) reserved for clients with a contract. Come back later.",
                            reservedSpaces));
                    return;
                }

                // Add a car only if there are free spaces left
                context.Cars.Add(car);
            }
        }

        /// <summary>
        /// Calculate free spaces
        /// </summary>
        /// <param name="carsInParkingLotWithContract"></param>
        /// <param name="reservedSpaces"></param>
        /// <returns></returns>
        private int GetFreeSpaces(int carsInParkingLotWithContract, int reservedSpaces)
        {
            var freeSpaces = TotalParkingSpaces -
                             (carsInParkingLotWithContract + reservedSpaces + TotalFreeParkingSpaces);
            return freeSpaces;
        }

        /// <summary>
        /// Calculate reserved spaces
        /// </summary>
        /// <param name="carsInParkingLotWithContract"></param>
        /// <returns></returns>
        private int GetReservedSpaces(int carsInParkingLotWithContract)
        {
            var reservedSpaces = TotalContractParingSpaces - carsInParkingLotWithContract;
            return reservedSpaces < 0 ? 0 : reservedSpaces;
        }

        /// <summary>
        /// Checks if number of cars currently in parking lot take all the free spaces and
        /// if there are contract parking spaces left.
        /// </summary>
        /// <param name="carsInParkingLot"></param>
        /// <param name="carsInParkingLotWithContract"></param>
        /// <param name="hasContract"></param>
        /// <returns></returns>
        private static bool CanAddCarsWhenNoFreeSpaceLeft(int carsInParkingLot, int carsInParkingLotWithContract, bool hasContract)
        {
            return carsInParkingLot >= TotalFreeParkingSpaces && carsInParkingLotWithContract < TotalContractParingSpaces && hasContract;
        }

        /// <summary>
        /// Checks if Parking lot contains any free spaces
        /// </summary>
        /// <param name="carsInParkingLot"></param>
        /// <returns></returns>
        private bool HasFreeParkingSpaces(int carsInParkingLot)
        {
            return carsInParkingLot < TotalParkingSpaces;
        }
    }
}