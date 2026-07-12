# Lemon Labs

Lemon Labs is a meal-prepping platform that generates personalized daily meal plans from locally-sourced ingredients, or connects users with a local chef who specializes in their diet. Built as a final capstone project for TrueCoders.

Full project rationale, tech-stack decisions, and testing plan: [PROJECT_PROPOSAL.md](PROJECT_PROPOSAL.md).

## Purpose

Users tell the platform their diet type, allergies, daily calorie goal, and weekly budget. Lemon Labs then either:

1. **Generates a daily meal plan** from a recipe database, matching their diet, avoiding their allergens, and staying within their calorie and budget limits, or
2. **Connects them with a local chef** whose specialty matches their diet, through a booking system.

Every recipe traces its ingredients back to the local farm that supplies them.

## Features

- **Meal plan generator** — the `MealPlanService` filters recipes by diet compatibility (e.g. a vegetarian user can receive vegan or vegetarian recipes, not paleo), excludes any recipe containing a listed allergen, and selects a breakfast/lunch/dinner combination that stays within the user's daily calorie goal and daily budget.
- **Farm-to-recipe traceability** — Farms supply Ingredients, and Recipes list which Ingredients (and therefore which Farms) they're built from.
- **Chef booking** — browse chefs by dietary specialty and book one, with a status workflow (Requested → Confirmed → Completed/Cancelled).
- **Full CRUD** for Farms, Ingredients, Recipes, User Profiles, Chefs, and Bookings, with server-side validation and foreign-key dropdowns for related data.

## Tech Stack

- **Backend:** ASP.NET Core MVC (.NET 7), C#
- **Database:** SQLite via Entity Framework Core (Code First, migrations, seeded sample data)
- **Frontend:** Razor views, Bootstrap
- **Testing:** xUnit, with EF Core's in-memory provider for repository tests

## Project Structure

```
LemonLabs.Web/
  Controllers/     MVC controllers (one per entity, plus MealPlanController)
  Models/           EF Core entities: Farm, Ingredient, Recipe, RecipeIngredient,
                     UserProfile, Chef, Booking, and shared enums
  Data/             AppDbContext + seed data
  Repositories/     Data-access layer (one repository per entity)
  Services/         MealPlanService — the diet/allergy/budget matching engine
  Views/            Razor views (Index/Details/Create/Edit/Delete per entity)
  Migrations/       EF Core migrations
LemonLabs.Tests/
  MealPlanServiceTests.cs       Unit tests for the matching algorithm
  FarmRepositoryTests.cs        Repository CRUD tests
  RecipeRepositoryTests.cs      Repository CRUD tests
  UserProfileRepositoryTests.cs Repository CRUD tests
```

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)

### Run the app

```bash
cd LemonLabs.Web
dotnet run
```

The app applies EF Core migrations and seeds sample data (farms, ingredients, recipes, chefs) automatically on startup. Open the URL shown in the console (default `http://localhost:5171`).

### Try it out

1. Go to **Profiles** and create a user profile with a diet type, allergies, calorie goal, and weekly budget.
2. Go to **Meal Plan**, select your profile, and click **Generate** to see a matched daily plan.
3. Or go to **Chefs** and **Bookings** to book a chef matching your diet.

### Run the tests

```bash
dotnet test
```

21 xUnit tests cover the meal-plan matching algorithm and repository CRUD behavior.

## License

Student project — for educational/portfolio use.
