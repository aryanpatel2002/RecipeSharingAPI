using System;
using System.Linq;
using RecipeSharingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace RecipeSharingAPI.Data
{
    public static class DbInitializer
    {
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure DB exists
            Console.WriteLine("DbInitializer: Ensuring database is migrated...");
            context.Database.Migrate();
            Console.WriteLine("DbInitializer: Migration complete.");

            // Users
            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var userId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            Console.WriteLine("DbInitializer: Seeding users...");
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                context.Users.Add(new User
                {
                    Id = adminId,
                    Username = "admin",
                    Email = "admin@demo.local",
                    PasswordHash = HashPassword("AdminPass123"),
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                });
                Console.WriteLine("DbInitializer: Added admin user");
            }

            if (!context.Users.Any(u => u.Username == "demo"))
            {
                context.Users.Add(new User
                {
                    Id = userId,
                    Username = "demo",
                    Email = "demo@demo.local",
                    PasswordHash = HashPassword("UserPass123"),
                    Role = "User",
                    CreatedAt = DateTime.UtcNow
                });
                Console.WriteLine("DbInitializer: Added demo user");
            }

            // Categories
            var cat1 = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var cat2 = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var cat3 = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var cat4 = Guid.Parse("66666666-6666-6666-6666-666666666666");

            Console.WriteLine("DbInitializer: Seeding categories...");
            if (!context.Categories.Any(c => c.Name == "Dessert"))
                context.Categories.Add(new Category { Id = cat1, Name = "Dessert", Description = "Sweet treats", IsActive = true, CreatedAt = DateTime.UtcNow });
            if (!context.Categories.Any(c => c.Name == "Main Course"))
                context.Categories.Add(new Category { Id = cat2, Name = "Main Course", Description = "Hearty mains", IsActive = true, CreatedAt = DateTime.UtcNow });
            if (!context.Categories.Any(c => c.Name == "Appetizer"))
                context.Categories.Add(new Category { Id = cat3, Name = "Appetizer", Description = "Starters and snacks", IsActive = true, CreatedAt = DateTime.UtcNow });
            if (!context.Categories.Any(c => c.Name == "Beverage"))
                context.Categories.Add(new Category { Id = cat4, Name = "Beverage", Description = "Refreshing drinks", IsActive = true, CreatedAt = DateTime.UtcNow });

            // Ingredients
            var ing1 = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var ing2 = Guid.Parse("88888888-8888-8888-8888-888888888888");
            var ing3 = Guid.Parse("99999999-9999-9999-9999-999999999999");
            var ing4 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            Console.WriteLine("DbInitializer: Seeding ingredients...");
            if (!context.Ingredients.Any(i => i.Name == "Sugar"))
                context.Ingredients.Add(new Ingredient { Id = ing1, Name = "Sugar", Description = "Sweetener", IsAllergen = false, Unit = "g" });
            if (!context.Ingredients.Any(i => i.Name == "Flour"))
                context.Ingredients.Add(new Ingredient { Id = ing2, Name = "Flour", Description = "Baking flour", IsAllergen = false, Unit = "g" });
            if (!context.Ingredients.Any(i => i.Name == "Butter"))
                context.Ingredients.Add(new Ingredient { Id = ing3, Name = "Butter", Description = "Creamy butter", IsAllergen = false, Unit = "g" });
            if (!context.Ingredients.Any(i => i.Name == "Egg"))
                context.Ingredients.Add(new Ingredient { Id = ing4, Name = "Egg", Description = "Chicken egg", IsAllergen = false, Unit = "pcs" });

            context.SaveChanges();
            Console.WriteLine("DbInitializer: Users/Categories/Ingredients saved.");

            // Recipes and relations
            var recipe1 = Guid.Parse("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var recipe2 = Guid.Parse("ccccccc1-cccc-cccc-cccc-cccccccccccc");
            var recipe3 = Guid.Parse("ddddddd1-dddd-dddd-dddd-dddddddddddd");

            Console.WriteLine("DbInitializer: Seeding recipes...");
            if (!context.Recipes.Any(r => r.Title == "Simple Cake"))
            {
                context.Recipes.Add(new Recipe
                {
                    Id = recipe1,
                    Title = "Simple Cake",
                    Description = "A very simple cake for demos",
                    CreationDate = DateTime.UtcNow,
                    CreatedById = userId
                });
            }

            if (!context.Recipes.Any(r => r.Title == "Chocolate Brownie"))
            {
                context.Recipes.Add(new Recipe
                {
                    Id = recipe2,
                    Title = "Chocolate Brownie",
                    Description = "Rich, fudgy brownies with cocoa and butter",
                    CreationDate = DateTime.UtcNow,
                    CreatedById = adminId
                });
            }

            if (!context.Recipes.Any(r => r.Title == "Pancake"))
            {
                context.Recipes.Add(new Recipe
                {
                    Id = recipe3,
                    Title = "Pancake",
                    Description = "Soft and fluffy pancakes",
                    CreationDate = DateTime.UtcNow,
                    CreatedById = userId
                });
            }

            context.SaveChanges();
            Console.WriteLine("DbInitializer: Recipes saved.");

            // RecipeCategory relations
            Console.WriteLine("DbInitializer: Seeding recipe-category relations...");
            if (!context.RecipeCategories.Any(rc => rc.RecipeId == recipe1 && rc.CategoryId == cat1))
                context.RecipeCategories.Add(new RecipeCategory { RecipeId = recipe1, CategoryId = cat1 });
            if (!context.RecipeCategories.Any(rc => rc.RecipeId == recipe2 && rc.CategoryId == cat1))
                context.RecipeCategories.Add(new RecipeCategory { RecipeId = recipe2, CategoryId = cat1 });
            if (!context.RecipeCategories.Any(rc => rc.RecipeId == recipe3 && rc.CategoryId == cat2))
                context.RecipeCategories.Add(new RecipeCategory { RecipeId = recipe3, CategoryId = cat2 });

            context.SaveChanges();
            Console.WriteLine("DbInitializer: RecipeCategory relations saved.");

            // RecipeIngredients
            Console.WriteLine("DbInitializer: Seeding recipe-ingredients...");
            if (!context.RecipeIngredients.Any(ri => ri.RecipeId == recipe1 && ri.IngredientId == ing1))
                context.RecipeIngredients.Add(new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe1, IngredientId = ing1, Quantity = 200f, Unit = "g", Notes = "For sweetness" });
            if (!context.RecipeIngredients.Any(ri => ri.RecipeId == recipe1 && ri.IngredientId == ing2))
                context.RecipeIngredients.Add(new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe1, IngredientId = ing2, Quantity = 300f, Unit = "g", Notes = "Base flour" });

            if (!context.RecipeIngredients.Any(ri => ri.RecipeId == recipe2 && ri.IngredientId == ing2))
                context.RecipeIngredients.Add(new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe2, IngredientId = ing2, Quantity = 200f, Unit = "g", Notes = "Batter base" });
            if (!context.RecipeIngredients.Any(ri => ri.RecipeId == recipe2 && ri.IngredientId == ing3))
                context.RecipeIngredients.Add(new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe2, IngredientId = ing3, Quantity = 100f, Unit = "g", Notes = "Adds richness" });
            if (!context.RecipeIngredients.Any(ri => ri.RecipeId == recipe2 && ri.IngredientId == ing1))
                context.RecipeIngredients.Add(new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe2, IngredientId = ing1, Quantity = 100f, Unit = "g", Notes = "Sweet taste" });

            if (!context.RecipeIngredients.Any(ri => ri.RecipeId == recipe3 && ri.IngredientId == ing2))
                context.RecipeIngredients.Add(new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe3, IngredientId = ing2, Quantity = 150f, Unit = "g", Notes = "Main structure" });
            if (!context.RecipeIngredients.Any(ri => ri.RecipeId == recipe3 && ri.IngredientId == ing4))
                context.RecipeIngredients.Add(new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe3, IngredientId = ing4, Quantity = 2f, Unit = "pcs", Notes = "Binding agent" });
            if (!context.RecipeIngredients.Any(ri => ri.RecipeId == recipe3 && ri.IngredientId == ing3))
                context.RecipeIngredients.Add(new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe3, IngredientId = ing3, Quantity = 50f, Unit = "g", Notes = "Flavor and texture" });

            context.SaveChanges();
            Console.WriteLine("DbInitializer: RecipeIngredients saved. Seeding complete.");
        }
    }
}
