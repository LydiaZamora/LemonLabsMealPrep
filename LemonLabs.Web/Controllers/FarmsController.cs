using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LemonLabs.Web.Controllers
{
    public class FarmsController : Controller
    {
        private readonly IFarmRepository _repository;

        public FarmsController(IFarmRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var farm = await _repository.GetByIdAsync(id);
            if (farm == null) return NotFound();
            return View(farm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Location,Description")] Farm farm)
        {
            if (!ModelState.IsValid) return View(farm);
            await _repository.AddAsync(farm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var farm = await _repository.GetByIdAsync(id);
            if (farm == null) return NotFound();
            return View(farm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Description")] Farm farm)
        {
            if (id != farm.Id) return NotFound();
            if (!ModelState.IsValid) return View(farm);
            await _repository.UpdateAsync(farm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var farm = await _repository.GetByIdAsync(id);
            if (farm == null) return NotFound();
            return View(farm);
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
