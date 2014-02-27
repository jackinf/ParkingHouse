using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Entities;

namespace ParkingHouse.Controllers
{
    public class ParkingController : Controller
    {

        private ICarsParkingRepository _repository;
        private static double _sum = 0;
        private static uint _totalCars = 0;
        private static int _carsParking = 0;
        private static bool _endSimulation;

        public ParkingController(ICarsParkingRepository repo)
        {
            _endSimulation = false;
            _repository = repo;
        }

        public ActionResult List()
        {
            ViewBag.Sum = _sum;
            ViewBag.Cars = _carsParking;
            ViewBag.TotalCars = _totalCars;
            if (_endSimulation)
            {                
                _sum = 0;
                _totalCars = 0;                
            }
            return View(_repository.Cars);
        }

        public ActionResult RemoveParkingCar(int id)
        {            

            if (ModelState.IsValid)
            {
                _sum += _repository.RemoveCar(id);
                _totalCars++;
                _carsParking--;
                TempData["message"] = "A vehicle has left the parking house!";
                return RedirectToAction("List", new{ id = _sum});
            }
            else
            {
                TempData["message"] = "The vehicle has broken down.";
                return RedirectToAction("List");
            }           
        }

        [HttpPost]
        public ActionResult AddParkingCar(Car car)
        {
  
            if (ModelState.IsValid)
            {
                _repository.AddCar(car);
                _carsParking = _repository.Cars.Count();
                TempData["message"] = "A vehicle has entered the parking house!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["message"] = "The vehicle does not fit.";
                return RedirectToAction("List");
            }
        }

        public ActionResult EndSimulation()
        {
            foreach (var car in _repository.Cars.ToList())
            {
                RemoveParkingCar(car.CarID);
            }            
            TempData["message"] = "Simulation ended! All vehicles have left.";
            _endSimulation = true;            
            return RedirectToAction("List");
        }
    }
}
