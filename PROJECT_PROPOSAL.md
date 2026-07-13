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

- **Unit testing for backend logic using xUnit:** 21 test cases in `LemonLabs.Tests` cover the `MealPlanService` matching algorithm — diet-compatibility filtering, allergen exclusion, calorie and budget ceilings, diet-hierarchy behavior (e.g., vegetarians can receive vegan recipes), null-argument handling, and empty-result behavior when no recipes match.
- **Repository-layer testing:** `FarmRepositoryTests`, `RecipeRepositoryTests`, and `UserProfileRepositoryTests` verify create, read, update, delete, and existence-check behavior against EF Core's in-memory provider, each isolated with its own database instance.
- **Manual end-to-end verification:** key user flows (creating a profile, generating a meal plan, booking a chef, full CRUD on every entity) were manually tested in a browser during development, including form validation and foreign-key relationships.
- **Not included in this version:** no automated integration test suite and no formal user acceptance testing with outside users — both are realistic next steps but outside the scope of this submission.

All 21 automated tests pass with `dotnet test`, and the full solution builds with zero warnings and zero errors.

## Deployment

- **Current state:** the app runs locally via `dotnet run` against a file-based SQLite database; it has not been deployed to a public host for this submission.
- **Server requirements:** any environment with the .NET 7 runtime; EF Core migrations apply automatically at startup, so no manual database setup is needed.
- **Path to production hosting:** deploying to Azure App Service would mean swapping the EF Core provider from SQLite to SQL Server/Azure SQL and setting the connection string via app configuration — no other code changes required.
- **Scalability considerations:** not applicable at the current student-project scale; the repository/service layering would support adding caching or read replicas later without touching controllers or views.

## Expected Outcome

- A fully functional meal-prepping web application, runnable locally, where a user can create a profile, generate a personalized daily meal plan sourced from local farms, and book a chef matching their diet.
- **Success criteria:** full CRUD across all six entities with no errors, a working diet/allergy/budget-matching algorithm, and a passing automated test suite (21/21) — all of which are met as of this submission.

## Conclusion

Lemon Labs demonstrates the core skills of the course — ASP.NET Core MVC, Entity Framework Core, the repository pattern, and automated testing — applied to a problem domain (personalized, locally-sourced meal planning) with a real decision-making feature at its center rather than pure data entry.

## References

- [1] Microsoft Docs, *ASP.NET Core MVC Overview* — https://learn.microsoft.com/aspnet/core/mvc/overview
- [2] Microsoft Docs, *Entity Framework Core* — https://learn.microsoft.com/ef/core
- [3] xUnit.net Documentation — https://xunit.net
