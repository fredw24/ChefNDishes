using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefNDishes.Controllers
{
    public class HomeController : Controller
    {

        private FoodContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(FoodContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            List<Chef> AllChefs = dbContext.chef.Include(u=> u.Dishes).ToList();
            
            for (int i = 0; i< AllChefs.Count; i++){
                AllChefs[i].DateToAge(AllChefs[i].Age);
            }

            ViewBag.chef = AllChefs;

            return View();
        }

        [Route("dishes")]
        [HttpGet]
        public IActionResult Dishes()
        {
            List<Dish> AllDishes = dbContext.dishes.Include(u=> u.myChef).ToList();

            ViewBag.dish = AllDishes;
            return View();
        }

        [Route("dishes/new")]
        [HttpGet]
        public IActionResult DishesNew()
        {
            List<Chef> AllChefs = dbContext.chef.ToList();

            ViewBag.cheflist = AllChefs;


            return View();
        }

        [HttpPost("dishes/new")]
        public IActionResult DishesMake(Dish dish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(dish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                List<Chef> AllChefs = dbContext.chef.ToList();

                ViewBag.cheflist = AllChefs;
                
                return View("DishesNew");
            }
        }
        
        [Route("/new")]
        [HttpGet]
        public IActionResult ChefNew()
        {
            return View();
        }

        [HttpPost("/new")]
        public IActionResult AddChef(Chef chef){
            if (ModelState.IsValid)
            {
                dbContext.Add(chef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("ChefNew");
            }

            
        }


    }
}
