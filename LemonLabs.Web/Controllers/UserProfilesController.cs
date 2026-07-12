using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LemonLabs.Web.Controllers
{
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileRepository _repository;

        public UserProfilesController(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var profile = await _repository.GetByIdAsync(id);
            if (profile == null) return NotFound();
            return View(profile);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,DietType,Allergies,DailyCalorieGoal,WeeklyBudget")] UserProfile profile)
        {
            if (!ModelState.IsValid) return View(profile);
            await _repository.AddAsync(profile);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var profile = await _repository.GetByIdAsync(id);
            if (profile == null) return NotFound();
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,DietType,Allergies,DailyCalorieGoal,WeeklyBudget")] UserProfile profile)
        {
            if (id != profile.Id) return NotFound();
            if (!ModelState.IsValid) return View(profile);
            await _repository.UpdateAsync(profile);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var profile = await _repository.GetByIdAsync(id);
            if (profile == null) return NotFound();
            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
