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
        public int PageSize = 10;

        public ParkingController(ICarsParkingRepository repo)
        {
            _repository = repo;
        }
        public ViewResult List()
        {
            return View(_repository.Cars);
        }

        public ActionResult RemoveParkingCar(int id)
        {            

            if (ModelState.IsValid)
            {
                _repository.RemoveCar(id);
                TempData["message"] = "A vehicle has left the parking house!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["message"] = "The vehicle has broken down.";
                return View("List");
            }           
        }

        [HttpPost]
        public ActionResult AddParkingCar(Car car)
        {
            if (ModelState.IsValid)
            {
                _repository.AddCar(car);
                TempData["message"] = "A vehicle has entered the parking house!";
                return RedirectToAction("List");
            }
            else
            {
                TempData["message"] = "The vehicle does not fit.";
                return View("List");
            }
        }
    }
}
