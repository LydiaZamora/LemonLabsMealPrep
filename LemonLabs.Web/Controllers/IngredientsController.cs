using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LemonLabs.Web.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IIngredientRepository _repository;
        private readonly IFarmRepository _farmRepository;

        public IngredientsController(IIngredientRepository repository, IFarmRepository farmRepository)
        {
            _repository = repository;
            _farmRepository = farmRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var ingredient = await _repository.GetByIdAsync(id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateFarmsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsOrganic,SeasonalAvailability,FarmId")] Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                await PopulateFarmsAsync(ingredient.FarmId);
                return View(ingredient);
            }
            await _repository.AddAsync(ingredient);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await _repository.GetByIdAsync(id);
            if (ingredient == null) return NotFound();
            await PopulateFarmsAsync(ingredient.FarmId);
            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsOrganic,SeasonalAvailability,FarmId")] Ingredient ingredient)
        {
            if (id != ingredient.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                await PopulateFarmsAsync(ingredient.FarmId);
                return View(ingredient);
            }
            await _repository.UpdateAsync(ingredient);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _repository.GetByIdAsync(id);
            if (ingredient == null) return NotFound();
            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateFarmsAsync(int? selectedFarmId = null)
        {
            var farms = await _farmRepository.GetAllAsync();
            ViewBag.FarmId = new SelectList(farms, "Id", "Name", selectedFarmId);
        }
    }
}
