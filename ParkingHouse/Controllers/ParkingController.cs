using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkingHouse.DB.Abstract;
using ParkingHouse.DB.Entities;
using ParkingHouse.Models;

namespace ParkingHouse.Controllers
{
    public class ParkingController : Controller
    {

        private ICarsParkingRepository _repository;
        private ISummaryRepository _repositorySum;
      
        public ParkingController(ICarsParkingRepository repo, ISummaryRepository repoSum)
        {
            _repository = repo;
            _repositorySum = repoSum;
            ParkingLot.CarsParking = _repository.Cars.Count();
            ParkingLot.Sum = (double)(from database in _repositorySum.CurrentProfits orderby database.ID descending select database.CurrentSum).FirstOrDefault();
            ParkingLot.TotalCars = (int)(from database in _repositorySum.CurrentProfits orderby database.ID descending select database.TotalCars).FirstOrDefault();
        }

        public ActionResult List()
        {
            if (ParkingLot.TemporarySum != 0.0 && ParkingLot.TemporaryTotalCars != 0)
            {
                TempData["summarySum"] = ParkingLot.TemporarySum;
                TempData["summaryCars"] = ParkingLot.TemporaryTotalCars;
                ParkingLot.TemporaryTotalCars = 0;
                ParkingLot.TemporarySum = 0;
            }            
            ViewBag.Sum = ParkingLot.Sum;
            ViewBag.Cars = ParkingLot.CarsParking;
            ViewBag.TotalCars = ParkingLot.TotalCars;

            return View(_repository.Cars);
        }

        public ActionResult RemoveParkingCar(int id)
        {            

            if (ModelState.IsValid)
            {
                ParkingLot.Sum += _repository.RemoveCar(id);                
                ParkingLot.TotalCars++;
                ParkingLot.CarsParking--;
                _repositorySum.SaveSum(ParkingLot.Sum, ParkingLot.TotalCars);
                TempData["message"] = "A vehicle has left the parking house!";
                return RedirectToAction("List", new{ id = ParkingLot.Sum});
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
            _repository.CheckForErrors(car, this);
            if (ModelState.IsValid)
            {
                _repository.AddCar(car);
                ParkingLot.CarsParking = _repository.Cars.Count();
                TempData["message"] = "A vehicle has entered the parking house!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["message"] = "The vehicle does not fit.";
                TempData["error"] = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
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
            ParkingLot.TemporarySum = ParkingLot.Sum;
            ParkingLot.TemporaryTotalCars = ParkingLot.TotalCars;
            _repositorySum.RemoveAll();
            ParkingLot.Reset();         
            return RedirectToAction("List");
        }
    }
}
