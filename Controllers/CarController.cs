using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wheels_Away.Data;
using Wheels_Away.Data.Enums;
using Wheels_Away.Helper;
using Wheels_Away.Models;
using Wheels_Away.Models.ViewModels;

namespace Wheels_Away.Controllers
{
	public class CarController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _usermanager;

		public CarController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
		{
			_context = context;
			_usermanager = usermanager;
		}
		public IActionResult SelectCar()
		{
			var cars = _context.Cars.ToList();

			return View(cars);
		}
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SelectCar(int SelectedCarId)
		{
			var bookingDetailsJson = HttpContext.Session.GetString("BookingDetails");
			if (bookingDetailsJson != null)
			{
				var bookingVM = JsonConvert.DeserializeObject<BookingVM>(bookingDetailsJson);
				var user = await _usermanager.GetUserAsync(User);
				var booking = new Booking
				{
					UserId = user.Id, 
					CarId = SelectedCarId,
					PickupLocation = bookingVM.PickupLocation,
					PickupDate = bookingVM.PickupDate,
					PickupTime = bookingVM.PickupTime,
					DropLocation = bookingVM.DropLocation,
					DropDate = bookingVM.DropDate,
					DropTime = bookingVM.DropTime,
				};
				HttpContext.Session.SetObjectAsJson("BookingDetails", booking);
				return View("Confirm", booking);
			}
			return RedirectToAction("Create", "Booking");
		}
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.FuelTypes = Enum.GetValues(typeof(FuelType)).Cast<FuelType>();
            ViewBag.GearTypes = Enum.GetValues(typeof(GearType)).Cast<GearType>();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                _context.SaveChanges();

                return RedirectToAction("SelectCar", "Car");
            }

            return View(car);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int carId)
        {
            var car = _context.Cars.Find(carId);

            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

            return RedirectToAction("SelectCar", "Car");
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                var existingCar = _context.Cars.Find(car.Id);

                if (existingCar != null)
                {
                    existingCar.RentalPrice = car.RentalPrice;
                    _context.SaveChanges();
                }

                return RedirectToAction("SelectCar", "Car");
            }

            return View(car);
        }
    }
}
