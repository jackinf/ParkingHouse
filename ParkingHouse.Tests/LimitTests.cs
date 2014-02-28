using System;
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
        [TestMethod]
        public void TestLimitCap()
        {
            Car car = new Car
            {
                CarID = 501,
                CarWidth = 2,
                CarLength = 4,
                HasContract = true
            };
            List<Car> cars = new List<Car>();
            for (int i = 0; i < 500; i++)
            {
                 Car repocar = new Car
                 {
                     CarID = i,
                     CarLength = 4,
                     CarWidth = 2,
                     HasContract = true
                 };
                cars.Add(repocar);
            }
            Mock<ICarsParkingRepository> mock = new Mock<ICarsParkingRepository>();
            mock.Setup(m => m.Cars).Returns(cars.AsQueryable());

            ParkingController target = new ParkingController(mock.Object);            
            ActionResult cannotAddCars = target.AddParkingCar(car);

            Assert.IsNotNull(cannotAddCars);
            Assert.AreEqual(500, cars.Count());
            
            ActionResult result = target.List();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void TestReservedParkingSpaces()
        {
            Car car = new Car
            {
                CarID = 501,
                CarWidth = 2,
                CarLength = 4,
                HasContract = true
            };
            List<Car> cars = new List<Car>();
            for (int i = 0; i < 450; i++)
            {
                Car repocar = new Car
                {
                    CarID = i,
                    CarLength = 4,
                    CarWidth = 2,
                    HasContract = false
                };
                cars.Add(repocar);
            }
            Mock<ICarsParkingRepository> mock = new Mock<ICarsParkingRepository>();
            mock.Setup(m => m.Cars).Returns(cars.AsQueryable());
            ParkingController target = new ParkingController(mock.Object);
            target.AddParkingCar(car);

        }  
    }
}
