using Microsoft.EntityFrameworkCore;
using RecipeSharingAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace RecipeSharingAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship configs
            modelBuilder.Entity<RecipeCategory>()
                .HasKey(rc => new { rc.RecipeId, rc.CategoryId });

            modelBuilder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Recipe)
                .WithMany(r => r.RecipeCategories)
                .HasForeignKey(rc => rc.RecipeId);

            modelBuilder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Category)
                .WithMany(c => c.RecipeCategories)
                .HasForeignKey(rc => rc.CategoryId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);

            // Local helper to hash passwords
            static string HashPassword(string password)
            {
                using var sha256 = SHA256.Create();
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }

            // ----------------------- USERS -----------------------
            var adminId = new Guid("11111111-1111-1111-1111-111111111111");
            var userId = new Guid("22222222-2222-2222-2222-222222222222");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    Username = "admin",
                    Email = "admin@demo.local",
                    PasswordHash = HashPassword("AdminPass123"),
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = userId,
                    Username = "demo",
                    Email = "demo@demo.local",
                    PasswordHash = HashPassword("UserPass123"),
                    Role = "User",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // ----------------------- CATEGORIES -----------------------
            var cat1 = new Guid("33333333-3333-3333-3333-333333333333");
            var cat2 = new Guid("44444444-4444-4444-4444-444444444444");
            var cat3 = new Guid("55555555-5555-5555-5555-555555555555");
            var cat4 = new Guid("66666666-6666-6666-6666-666666666666");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = cat1, Name = "Dessert", Description = "Sweet treats", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Category { Id = cat2, Name = "Main Course", Description = "Hearty mains", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Category { Id = cat3, Name = "Appetizer", Description = "Starters and snacks", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Category { Id = cat4, Name = "Beverage", Description = "Refreshing drinks", IsActive = true, CreatedAt = DateTime.UtcNow }
            );

            // ----------------------- INGREDIENTS -----------------------
            var ing1 = new Guid("77777777-7777-7777-7777-777777777777");
            var ing2 = new Guid("88888888-8888-8888-8888-888888888888");
            var ing3 = new Guid("99999999-9999-9999-9999-999999999999");
            var ing4 = new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = ing1, Name = "Sugar", Description = "Sweetener", IsAllergen = false, Unit = "g" },
                new Ingredient { Id = ing2, Name = "Flour", Description = "Baking flour", IsAllergen = false, Unit = "g" },
                new Ingredient { Id = ing3, Name = "Butter", Description = "Creamy butter", IsAllergen = false, Unit = "g" },
                new Ingredient { Id = ing4, Name = "Egg", Description = "Chicken egg", IsAllergen = false, Unit = "pcs" }
            );

            // ----------------------- RECIPES -----------------------
            var recipe1 = new Guid("bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var recipe2 = new Guid("ccccccc1-cccc-cccc-cccc-cccccccccccc");
            var recipe3 = new Guid("ddddddd1-dddd-dddd-dddd-dddddddddddd");

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = recipe1,
                    Title = "Simple Cake",
                    Description = "A very simple cake for demos",
                    CreationDate = DateTime.UtcNow,
                    CreatedById = userId
                },
                new Recipe
                {
                    Id = recipe2,
                    Title = "Chocolate Brownie",
                    Description = "Rich, fudgy brownies with cocoa and butter",
                    CreationDate = DateTime.UtcNow,
                    CreatedById = adminId
                },
                new Recipe
                {
                    Id = recipe3,
                    Title = "Pancake",
                    Description = "Soft and fluffy pancakes",
                    CreationDate = DateTime.UtcNow,
                    CreatedById = userId
                }
            );

            // ----------------------- RECIPE-CATEGORY RELATIONS -----------------------
            modelBuilder.Entity<RecipeCategory>().HasData(
                new RecipeCategory { RecipeId = recipe1, CategoryId = cat1 },
                new RecipeCategory { RecipeId = recipe2, CategoryId = cat1 },
                new RecipeCategory { RecipeId = recipe3, CategoryId = cat2 }
            );

            // ----------------------- RECIPE-INGREDIENTS -----------------------
            modelBuilder.Entity<RecipeIngredient>().HasData(
                new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe1, IngredientId = ing1, Quantity = 200f, Unit = "g", Notes = "For sweetness" },
                new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe1, IngredientId = ing2, Quantity = 300f, Unit = "g", Notes = "Base flour" },
                new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe2, IngredientId = ing2, Quantity = 200f, Unit = "g", Notes = "Batter base" },
                new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe2, IngredientId = ing3, Quantity = 100f, Unit = "g", Notes = "Adds richness" },
                new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe2, IngredientId = ing1, Quantity = 100f, Unit = "g", Notes = "Sweet taste" },
                new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe3, IngredientId = ing2, Quantity = 150f, Unit = "g", Notes = "Main structure" },
                new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe3, IngredientId = ing4, Quantity = 2f, Unit = "pcs", Notes = "Binding agent" },
                new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = recipe3, IngredientId = ing3, Quantity = 50f, Unit = "g", Notes = "Flavor and texture" }
            );
        }
    }
}
