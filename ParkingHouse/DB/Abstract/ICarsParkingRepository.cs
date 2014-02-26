using System.Linq;
using ParkingHouse.DB.Entities;

namespace ParkingHouse.DB.Abstract
{
    public interface ICarsParkingRepository
    {
        IQueryable<Car> Cars { get; }
        void AddCar(Car car);

        void RemoveCar(int carId);
    }
}
