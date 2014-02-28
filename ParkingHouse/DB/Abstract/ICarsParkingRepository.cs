using System.Linq;
using ParkingHouse.Controllers;
using ParkingHouse.DB.Entities;

namespace ParkingHouse.DB.Abstract
{
    public interface ICarsParkingRepository
    {
        IQueryable<Car> Cars { get; }
        double RemoveCar(int carId);
        void AddCar(Car car);
        void CheckForErrors(Car car, ParkingController parkingController);
    }
}
