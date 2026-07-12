using LemonLabs.Web.Models;
using LemonLabs.Web.Repositories;
using LemonLabs.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LemonLabs.Web.Controllers
{
    public class MealPlanController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMealPlanService _mealPlanService;

        public MealPlanController(
            IUserProfileRepository userProfileRepository,
            IRecipeRepository recipeRepository,
            IMealPlanService mealPlanService)
        {
            _userProfileRepository = userProfileRepository;
            _recipeRepository = recipeRepository;
            _mealPlanService = mealPlanService;
        }

        public async Task<IActionResult> Index(int? userProfileId)
        {
            var profiles = await _userProfileRepository.GetAllAsync();
            ViewBag.UserProfileId = new SelectList(profiles, "Id", "Name", userProfileId);

            if (userProfileId == null)
            {
                return View();
            }

            var profile = await _userProfileRepository.GetByIdAsync(userProfileId.Value);
            if (profile == null) return NotFound();

            var recipes = await _recipeRepository.GetAllAsync();
            var plan = _mealPlanService.GenerateDailyPlan(profile, recipes);

            ViewBag.Profile = profile;
            return View(plan);
        }
    }
}
