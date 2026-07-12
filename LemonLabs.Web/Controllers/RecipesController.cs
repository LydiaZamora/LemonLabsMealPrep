using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LemonLabs.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository _repository;

        public RecipesController(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _repository.GetByIdAsync(id);
            if (recipe == null) return NotFound();
            return View(recipe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,DietType,Category,Calories,PricePerServing,Allergens")] Recipe recipe)
        {
            if (!ModelState.IsValid) return View(recipe);
            await _repository.AddAsync(recipe);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _repository.GetByIdAsync(id);
            if (recipe == null) return NotFound();
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DietType,Category,Calories,PricePerServing,Allergens")] Recipe recipe)
        {
            if (id != recipe.Id) return NotFound();
            if (!ModelState.IsValid) return View(recipe);
            await _repository.UpdateAsync(recipe);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _repository.GetByIdAsync(id);
            if (recipe == null) return NotFound();
            return View(recipe);
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
