using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using RentalCars.Models;

namespace RentalCars.Controllers
{
    public class BookingController : Controller
    {
        [HttpGet]
        public IActionResult Create(int carId, int userId)
        {
            IEnumerable<Car> cars = CarsForBooking();
            Car car = cars.Single(x => x.CarId == carId);
            if(car == null || !car.IsAvailable)
            {
                return NotFound("The car is not available.");
            }

            return View(new Booking { BookingId = 1, CarId = carId, Car = car, StartDate = DateTime.Now, EndDate =DateTime.Now.AddDays(1), TotalPrice = car.PricePerDay});
        }
        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            IEnumerable<Car> cars = CarsForBooking();
            Car car = cars.SingleOrDefault(x => x.CarId == booking.CarId);
            if (car == null || !car.IsAvailable)
            {
                return NotFound("The car is not available.");
            }
            var rentalDays = (booking.EndDate - booking.StartDate).Days;
            booking.TotalPrice = rentalDays * car.PricePerDay;
            booking.Car = car;

            if (ModelState.IsValid)
            {
                return RedirectToAction("Confirmation", new { bookingId = booking.BookingId });
            }
            return View(booking);
        }
        public IActionResult Confirmation(int bookingId)
        {
            IEnumerable<Booking> bookings = BookingOptions();
            Booking booking = bookings.SingleOrDefault(x => x.BookingId == bookingId);

            if (booking ==  null)
            {
                return NotFound("Booking not found.");
            }
            booking.Car = CarsForBooking().SingleOrDefault(x => x.CarId == booking.CarId);
            return View(booking);
        }
        public IActionResult Return()
        {
            return View();
        }
        public IActionResult History(int userId)
        {
            return View();
        }

        private IEnumerable<Car> CarsForBooking()
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

        private IEnumerable<Booking> BookingOptions()
        {
            List<Booking> bookings = new List<Booking>();

            bookings.Add(new Booking()
            {
                BookingId =1,
                CarId = 4,
                UserId = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddHours(4),
                TotalPrice = 55.5m,

                Car = new Car()
                {
                    CarId = 4,
                    Make = "Mercedes",
                    Model = "M7",
                    PricePerDay =55.5m,
                    IsAvailable = true,
                    ImageUrl = "mercedesyellow.jpg"
                }
            });
                bookings.Add(new Booking()
                {
                    BookingId =2,
                    CarId = 4,
                    UserId = 1,
                    StartDate = DateTime.Now.AddDays(2),
                    EndDate = DateTime.Now.AddHours(4),
                    TotalPrice = 111,

                    Car = new Car()
                    {
                        CarId = 4,
                        Make = "Mercedes",
                        Model = "M7",
                        PricePerDay =55.5m,
                        IsAvailable = true,
                        ImageUrl = "mercedesyellow.jpg"
                    }
                });
        }   

    }
}
