using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LemonLabs.Web.Controllers
{
    public class ChefsController : Controller
    {
        private readonly IChefRepository _repository;

        public ChefsController(IChefRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var chef = await _repository.GetByIdAsync(id);
            if (chef == null) return NotFound();
            return View(chef);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Bio,Specialty,YearsExperience,HourlyRate")] Chef chef)
        {
            if (!ModelState.IsValid) return View(chef);
            await _repository.AddAsync(chef);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var chef = await _repository.GetByIdAsync(id);
            if (chef == null) return NotFound();
            return View(chef);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Bio,Specialty,YearsExperience,HourlyRate")] Chef chef)
        {
            if (id != chef.Id) return NotFound();
            if (!ModelState.IsValid) return View(chef);
            await _repository.UpdateAsync(chef);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var chef = await _repository.GetByIdAsync(id);
            if (chef == null) return NotFound();
            return View(chef);
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
