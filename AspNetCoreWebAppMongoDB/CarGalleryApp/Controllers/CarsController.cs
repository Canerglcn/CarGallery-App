using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarGalleryApp.Models;
using CarGalleryApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarGalleryApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }

        // GET: Cars
        public ActionResult Index()
        {
            return View(_carService.Get());
        }

        // GET: Cars/Details/5
        public ActionResult Details(string id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var car = _carService.Get(id);
            if(car==null)
            {
                return NotFound();
            }
        

            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post: Cars/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if(ModelState.IsValid)
            {
                _carService.Create(car);
                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }


        // GET: Cars/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = _carService.Get(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id,Car car)
        {
            if (id != car.Id)
                return NotFound();
            if(ModelState.IsValid)
            {
                _carService.Update(id, car);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(car);
            }
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
                return NotFound();

            var car = _carService.Get(id);
            if(car==null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var car = _carService.Get(id);
                if (car == null)
                    return NotFound();
                _carService.Remove(car.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}