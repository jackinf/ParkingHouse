
namespace ParkingHouse.Models
{
    public class ParkingLot
    {
        public static double Sum = 0;
        public static int TotalCars = 0;
        public static int CarsParking = 0;
        public static double TemporarySum = 0;
        public static int TemporaryTotalCars = 0;

        public static void Reset()
        {
            Sum = 0;
            TotalCars = 0;
            CarsParking = 0;
        }
    }
}