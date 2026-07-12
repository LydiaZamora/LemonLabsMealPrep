using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LemonLabs.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingRepository _repository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IChefRepository _chefRepository;

        public BookingsController(IBookingRepository repository, IUserProfileRepository userProfileRepository, IChefRepository chefRepository)
        {
            _repository = repository;
            _userProfileRepository = userProfileRepository;
            _chefRepository = chefRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserProfileId,ChefId,RequestedDate,Status,Notes")] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(booking.UserProfileId, booking.ChefId);
                return View(booking);
            }
            await _repository.AddAsync(booking);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null) return NotFound();
            await PopulateDropdownsAsync(booking.UserProfileId, booking.ChefId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserProfileId,ChefId,RequestedDate,Status,Notes")] Booking booking)
        {
            if (id != booking.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(booking.UserProfileId, booking.ChefId);
                return View(booking);
            }
            await _repository.UpdateAsync(booking);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _repository.GetByIdAsync(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownsAsync(int? selectedUserProfileId = null, int? selectedChefId = null)
        {
            var profiles = await _userProfileRepository.GetAllAsync();
            var chefs = await _chefRepository.GetAllAsync();
            ViewBag.UserProfileId = new SelectList(profiles, "Id", "Name", selectedUserProfileId);
            ViewBag.ChefId = new SelectList(chefs, "Id", "Name", selectedChefId);
        }
    }
}
