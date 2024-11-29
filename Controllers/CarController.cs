using Microsoft.AspNetCore.Mvc;
using RentalCars.Models;
using System.Reflection.Metadata.Ecma335;

namespace RentalCars.Controllers
{
    public class CarController : Controller
    {
        public async Task<IActionResult> Index(string make, string model, decimal? minPrice, decimal? maxPrice)
        {

            IEnumerable<Car> cars = InitCars();

            if (!string.IsNullOrEmpty(make))
            {
                cars = cars.Where(c => c.Make.ToLower() == make.ToLower().Trim());
            }
            if (!string.IsNullOrEmpty(model))
            {
                cars = cars.Where(c => c.Model.ToLower() == model.ToLower().Trim());
            }

                if (minPrice.HasValue)
                {
                    cars = cars.Where(c => c.PricePerDay >= minPrice.Value);
                }
                if (maxPrice.HasValue)
                {
                    cars = cars.Where(c => c.PricePerDay <= maxPrice.Value);
                }
                return View(cars);
        }
            public async Task<IActionResult> Details(int id)
            {
                var cars = InitCars();

                var car = cars.FirstOrDefault(c => c.CarId == id);

                if (car == null) return NotFound();

                return View(car);
            }


            private IEnumerable<Car> InitCars()
            {
                var cars = new List<Car>();

                Car A4 = new Car()
                {
                    CarId = 1,
                    Make = "Audi",
                    Model = "A4",
                    PricePerDay =30.5m,
                    IsAvailable = true,
                    ImageUrl = "audiblue.jpg"

                };
                Car B5 = new Car()
                {
                    CarId = 2,
                    Make = "BMW",
                    Model = "B5",
                    PricePerDay =50.5m,
                    IsAvailable = false,
                    ImageUrl = "bmwblue.jpg"

                };

                Car M6 = new Car()
                {
                    CarId = 3,
                    Make = "Mercedes",
                    Model = "M6",
                    PricePerDay =40.5m,
                    IsAvailable = false,
                    ImageUrl = "mercedesgrey.jpg"

                };
                Car M7 = new Car()
                {
                    CarId = 4,
                    Make = "Mercedes",
                    Model = "M7",
                    PricePerDay =55.5m,
                    IsAvailable = true,
                    ImageUrl = "mercedesyellow.jpg"

                };

                cars.Add(A4);
                cars.Add(B5);
                cars.Add(M6);
                cars.Add(M7);

                return cars;
            }
        

    }
}

