using Microsoft.EntityFrameworkCore;
 
namespace ChefNDishes.Models
{
    public class FoodContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public FoodContext(DbContextOptions<FoodContext> options) : base(options) { }

        public DbSet<Chef> chef {get; set;}

        public DbSet<Dish> dishes {get; set;}



    }
}