using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParkingHouse.Controllers;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Concrete;
using ParkingHouse.DB.Entities;


namespace ParkingHouse.Tests
{
    [TestClass]
    public class LimitTests
    {
        private ISummaryRepository Repository { get; set; }

        public LimitTests()
        {
            Repository = new EfCurrentProfitRepository();
        }

        [TestMethod]
        public void TestLimitCap()
        {
            var car = GenerateCar(501, 4, 2, true);
            var cars = GenerateCars(4, 2, true, 500);

            var mock = new Mock<ICarsParkingRepository>();
            var enumerable = cars as IList<Car> ?? cars.ToList();
            mock.Setup(m => m.Cars).Returns(enumerable.AsQueryable());

            var target = new ParkingController(mock.Object, Repository);            
            var cannotAddCars = target.AddParkingCar(car);

            Assert.IsNotNull(cannotAddCars);
            Assert.AreEqual(500, enumerable.Count());
            
            var result = target.List();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestReservedParkingSpaces()
        {
            var car = GenerateCar(501, 2, 4, true);
            var cars = GenerateCars(4, 2, false, 450);

            var mock = new Mock<ICarsParkingRepository>();
            mock.Setup(m => m.Cars).Returns(cars.AsQueryable());
            var target = new ParkingController(mock.Object, Repository);
            target.AddParkingCar(car);

        }

        /// <summary>
        /// Generates a single instance of a car
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="hasContract"></param>
        /// <returns></returns>
        private Car GenerateCar(int ID, decimal length, decimal width, bool hasContract)
        {
            var car = new Car
            {
                CarID = ID,
                CarWidth = length,
                CarLength = width,
                HasContract = hasContract
            };
            return car;
        }

        /// <summary>
        /// Generates multiple instances of car
        /// </summary>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="hasContract"></param>
        /// <param name="numberOfCars"></param>
        /// <returns></returns>
        private IEnumerable<Car> GenerateCars(decimal length, decimal width, bool hasContract, int numberOfCars)
        {
            var cars = new List<Car>();
            for (var i = 0; i < numberOfCars; i++)
            {
                var car = GenerateCar(i, length, width, hasContract);
                cars.Add(car);
            }
            return cars;
        }
    }
}
