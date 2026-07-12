using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LemonLabs.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Bio = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Specialty = table.Column<int>(type: "INTEGER", nullable: false),
                    YearsExperience = table.Column<int>(type: "INTEGER", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    DietType = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Calories = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerServing = table.Column<decimal>(type: "TEXT", nullable: false),
                    Allergens = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    DietType = table.Column<int>(type: "INTEGER", nullable: false),
                    Allergies = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    DailyCalorieGoal = table.Column<int>(type: "INTEGER", nullable: false),
                    WeeklyBudget = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsOrganic = table.Column<bool>(type: "INTEGER", nullable: false),
                    SeasonalAvailability = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FarmId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserProfileId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChefId = table.Column<int>(type: "INTEGER", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Chefs_ChefId",
                        column: x => x.ChefId,
                        principalTable: "Chefs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Chefs",
                columns: new[] { "Id", "Bio", "HourlyRate", "Name", "Specialty", "YearsExperience" },
                values: new object[,]
                {
                    { 1, "Plant-forward seasonal cooking.", 65m, "Chef Maria Alvarez", 2, 8 },
                    { 2, "High-protein performance meals.", 85m, "Chef Daniel Osei", 5, 12 }
                });

            migrationBuilder.InsertData(
                table: "Farms",
                columns: new[] { "Id", "Description", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Family-owned organic vegetable farm.", "Boulder, CO", "Green Valley Farm" },
                    { 2, "Pasture-raised poultry and eggs.", "Fort Collins, CO", "Sunrise Pastures" },
                    { 3, "Seasonal fruit orchard.", "Longmont, CO", "Rocky Creek Orchards" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Allergens", "Calories", "Category", "Description", "DietType", "Name", "PricePerServing" },
                values: new object[,]
                {
                    { 1, "None", 480, 1, "Roasted sweet potato over sauteed kale.", 2, "Kale & Sweet Potato Bowl", 8.50m },
                    { 2, "Eggs", 520, 0, "High-protein breakfast plate.", 5, "Grilled Chicken & Eggs Breakfast", 7.00m },
                    { 3, "Gluten", 350, 0, "Vegetarian oats with fresh blueberries.", 1, "Blueberry Overnight Oats", 5.25m }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "FarmId", "IsOrganic", "Name", "SeasonalAvailability" },
                values: new object[,]
                {
                    { 1, 1, true, "Kale", "Fall-Winter" },
                    { 2, 1, true, "Sweet Potato", "Fall" },
                    { 3, 2, true, "Free-Range Eggs", "Year-round" },
                    { 4, 2, false, "Chicken Breast", "Year-round" },
                    { 5, 3, true, "Blueberries", "Summer" }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "IngredientId", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 1, 1, "2 cups", 1 },
                    { 2, 2, "1 medium", 1 },
                    { 3, 3, "2 eggs", 2 },
                    { 4, 4, "4 oz", 2 },
                    { 5, 5, "1/2 cup", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ChefId",
                table: "Bookings",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserProfileId",
                table: "Bookings",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_FarmId",
                table: "Ingredients",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Chefs");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Farms");
        }
    }
}
