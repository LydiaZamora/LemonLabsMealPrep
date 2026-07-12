# Project Proposal: Lemon Labs

## Introduction

Lemon Labs is a meal-prepping platform that helps people eat well without doing the planning themselves. Users describe their diet, allergies, calorie goals, and budget, and the platform either generates a personalized daily meal plan built from locally-sourced ingredients or connects them with a local chef who specializes in their diet. The goal is to make healthy, locally-sourced eating accessible without requiring users to become nutrition experts or spend hours researching recipes.

## Objectives

- Build a data-driven web application that generates meal plans tailored to an individual's dietary restrictions, allergies, calorie goals, and budget.
- Model a supply chain that traces recipes back to the local farms supplying their ingredients, supporting the platform's "farm-to-table" value proposition.
- Give users a second path to healthy eating beyond self-service: booking a local chef whose specialty matches their diet.
- Demonstrate a full-stack ASP.NET Core MVC application with a real business-logic layer, not just CRUD forms.

## Project Description

Lemon Labs is built around six related entities: **Farms**, **Ingredients** (each sourced from a farm), **Recipes** (tagged by diet type, meal category, calories, price, and allergens), **User Profiles** (diet type, allergies, calorie goal, weekly budget), **Chefs** (diet specialty, experience, hourly rate), and **Bookings** (linking a user to a chef). Every recipe traces its ingredients back to the farm that supplies them, and the core matching engine — the `MealPlanService` — filters and scores recipes against a user's profile to produce a daily plan that respects diet compatibility, allergy exclusions, calorie goals, and budget.

## Technologies and Tools

- **Languages:** C# for backend logic, Razor (HTML/CSS) for views
- **Framework:** ASP.NET Core MVC (.NET 7)
- **ORM / Database:** Entity Framework Core with SQLite (Code First, migrations, seeded sample data)
- **Testing:** xUnit, with Entity Framework Core's in-memory provider for repository tests
- **Tools:** Visual Studio / VS Code, Git for version control

## Features

- **Diet-Aware Meal Plan Generator:** Given a user profile, the `MealPlanService` filters recipes by diet compatibility (e.g., a vegetarian eater can receive vegan or vegetarian recipes but not paleo ones), excludes any recipe containing a listed allergen, and greedily selects a breakfast/lunch/dinner combination that stays within the user's daily calorie goal and daily budget (weekly budget ÷ 7).
- **Local Farm Traceability:** Every ingredient is linked to the farm that supplies it, and every recipe lists the ingredients (and their source farms) it's built from.
- **Chef Booking:** Users who would rather not self-serve can browse chefs by dietary specialty and book one directly, with a booking status workflow (Requested → Confirmed → Completed/Cancelled).
- **Full CRUD Management:** Farms, Ingredients, Recipes, User Profiles, Chefs, and Bookings all support create, read, update, and delete through the MVC interface, with server-side validation and foreign-key dropdowns for related data.

## Implementation Plan

The project was built in five stages: (1) scaffold the ASP.NET Core MVC solution and a separate xUnit test project; (2) define the EF Core models, relationships, and seed data; (3) build the repository layer and the `MealPlanService` matching algorithm; (4) build controllers and Razor views for full CRUD across all six entities; (5) write unit tests and manually verify the application end-to-end in a browser.

## Challenges

The main design challenge was scoping the original vision — which included live cryptocurrency payments and third-party logistics integration — down to something buildable and testable within a course project timeline. Rather than dropping those ideas, the data model reflects them structurally (a `PricePerServing` field ready for a payment step, a `BookingStatus` workflow that mirrors a fulfillment pipeline) while leaving actual third-party integrations as documented future work rather than unverifiable stubs. A second challenge was designing a matching algorithm that was simple enough to unit test deterministically but still demonstrated real business logic beyond basic filtering — solved by combining diet-compatibility rules, allergy exclusion, and a greedy budget/calorie-constrained selection per meal category.

## User Interface (UI) Design

The UI uses Bootstrap for a clean, responsive layout consistent across all entity views (index tables, detail pages, and create/edit forms with client- and server-side validation). The meal-plan generation page is the centerpiece: a single dropdown to select a user profile produces a table of matched meals with running calorie and cost totals against the user's goals.

## Testing Plan

The test project (`LemonLabs.Tests`) contains 21 xUnit test cases across two layers:

- **Service-layer tests** (`MealPlanServiceTests`) verify the meal-plan matching algorithm in isolation: diet-compatibility filtering, allergen exclusion, calorie and budget ceilings, diet-hierarchy behavior (e.g., vegetarians can receive vegan recipes), null-argument handling, and empty-result behavior when no recipes match.
- **Repository-layer tests** (`FarmRepositoryTests`, `RecipeRepositoryTests`, `UserProfileRepositoryTests`) verify create, read, update, delete, and existence-check behavior against EF Core's in-memory database provider, isolated per test with a unique database instance.

All tests pass with `dotnet test`, and the full solution builds with zero warnings and zero errors.

## Deployment

The application is designed for straightforward deployment to any environment supporting .NET 7 and SQLite (or, for a hosted deployment, Azure App Service with EF Core's provider swapped to SQL Server/Azure SQL). EF Core migrations run automatically at startup (`db.Database.Migrate()`), so a fresh deployment only requires the app and its connection string.

## Expected Outcome

A fully functional meal-prepping web application where a user can create a profile, generate a personalized daily meal plan sourced from local farms, and book a chef matching their diet — backed by a tested, full-CRUD data layer.

## Conclusion

Lemon Labs demonstrates the core skills of the course — ASP.NET Core MVC, Entity Framework Core, the repository pattern, and automated testing — applied to a problem domain (personalized, locally-sourced meal planning) with a real decision-making feature at its center rather than pure data entry.

## References

- [1] Microsoft Docs, *ASP.NET Core MVC Overview* — https://learn.microsoft.com/aspnet/core/mvc/overview
- [2] Microsoft Docs, *Entity Framework Core* — https://learn.microsoft.com/ef/core
- [3] xUnit.net Documentation — https://xunit.net
