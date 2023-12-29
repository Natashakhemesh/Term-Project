using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using Wheels_Away.Data;
using Wheels_Away.Helper;
using Wheels_Away.Models;
using Wheels_Away.Models.ViewModels;

namespace Wheels_Away.Controllers
{
	public class BookingController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _usermanager;
		public BookingController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
		{
			_context = context;
			_usermanager = usermanager;

		}
		// GET: Booking/Create
		[AllowAnonymous]
		public IActionResult Create()
		{
			return View();
		}

		// POST: Booking/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(BookingVM bookingVM)
		{
			if (ModelState.IsValid)
			{
				HttpContext.Session.SetObjectAsJson("BookingDetails", bookingVM);

				return RedirectToAction("SelectCar", "Car");
			}
			return View(bookingVM);

		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> ConfirmBooking()
		{
			var bookingDetailsJson = HttpContext.Session.GetString("BookingDetails");
			if (bookingDetailsJson != null)
			{
				var booking = JsonConvert.DeserializeObject<Booking>(bookingDetailsJson);
				_context.Bookings.Add(booking);
				await _context.SaveChangesAsync();
				HttpContext.Session.Remove("BookingDetails");
				return Json(new { success = true });
			}
			return Json(new { success = false, error = "Booking details not found" });
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CancelBooking(int id)
		{
			if (HttpContext.Session.GetString("BookingDetails") != null)
			{
				HttpContext.Session.Remove("BookingDetails");
				HttpContext.Session.Remove("ChosenCarId");
			}
			var data = await _context.Bookings.FindAsync(id);
			if (data != null)
			{
				_context.Bookings.Remove(data);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction("Create", "Booking");

		}


		[Authorize]
		public async Task<IActionResult> Index()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var Bookings = await _context.Bookings.Where(B => B.UserId == userId).Include(b => b.User).Include(b => b.Car).ToListAsync();
			return View(Bookings);
		}
        [HttpGet]
        [Authorize] // You may want to add proper authorization based on your requirements
        public IActionResult Edit(int id)
        {
            var booking = _context.Bookings.Find(id);

            if (booking == null)
            {
                return NotFound();
            }

            var viewModel = new BookingVM
            {
                Id = booking.Id,
                PickupLocation = booking.PickupLocation,
                PickupDate = booking.PickupDate,
                PickupTime = booking.PickupTime,
                DropLocation = booking.DropLocation,
                DropDate = booking.DropDate,
                DropTime = booking.DropTime,
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize] 
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookingVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var booking = _context.Bookings.Find(viewModel.Id);

                if (booking == null)
                {
                    return NotFound();
                }

                booking.PickupLocation = viewModel.PickupLocation;
                booking.PickupDate = viewModel.PickupDate;
                booking.PickupTime = viewModel.PickupTime;
                booking.DropLocation = viewModel.DropLocation;
                booking.DropDate = viewModel.DropDate;
                booking.DropTime = viewModel.DropTime;

                _context.SaveChanges();

                return RedirectToAction("Index"); 
            }

            return View(viewModel);
        }


        [HttpPost]
        [Authorize] 
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Bookingid)
        {
            var booking = _context.Bookings.Find (Bookingid);

            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}


