using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;

    //add context
    private MyContext _context;    

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        _context= context;
    }
    //////GET ALL
    // [HttpGet("dishes")]-this would give us welcom page before getting here
    [HttpGet("")]
    public ViewResult AllDishes()
    {
        List<Dish> Dishes= _context.Dishes.OrderByDescending(d=>d.CreatedAt).ToList();
        //sending in the model to the Views/Dish/AllDishes.cshtml
        return View (Dishes);
    }

    //////Add New
    //-----get form-----
    [HttpGet("dishes/new")]
    public ViewResult NewDish()
    {
        return View("NewDish");
    }
    //---process form---
    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if(!ModelState.IsValid)
        {
            return View("NewDish");
        }
        _context.Add(newDish);
        _context.SaveChanges();
        return RedirectToAction ("Index", "Home");
    }

    ///GET ONE

    [HttpGet("dishes/{dishId}")]
    public IActionResult ViewDish(int dishId)
    {
        Dish? SingleDish= _context.Dishes.FirstOrDefault(d=>d.DishId==dishId);
        if (SingleDish == null){
            return RedirectToAction ("AllDishes");
        }
        //passing the model Single dish to the Views/Dish/ViewDish.cshtml
        return View (SingleDish);
    }
    //Edit- 2 routes: get, process
        //get
    [HttpGet("dishes/{dishId}/edit")]
    public IActionResult EditDish (int dishId)
    {
        Dish? ToBeEdited = _context.Dishes.FirstOrDefault (d=>d.DishId == dishId);
        if (ToBeEdited == null)
        {
            return RedirectToAction ("AllDishes");
        }
        return View (ToBeEdited);
    }
       //process the edits
    [HttpPost("dishes/{dishId}/update")]
    public IActionResult UpdateDish (int dishId, Dish editedDish)
    {
        Dish? ToBeUpdated = _context.Dishes.FirstOrDefault (d=>d.DishId == dishId);
        if (!ModelState.IsValid || ToBeUpdated == null)
        {
            return View ("EditDish", editedDish);
        }

        ToBeUpdated.Name= editedDish.Name;
        ToBeUpdated.Chef= editedDish.Chef;
        ToBeUpdated.Calories= editedDish.Calories;
        ToBeUpdated.Tastiness= editedDish.Tastiness;
        ToBeUpdated.Description= editedDish.Description;
        ToBeUpdated.UpdatedAt= DateTime.Now;
        _context.SaveChanges();

        // return RedirectToAction("AllDishes")// this redirects to All Dishes
        return RedirectToAction ("ViewDish", new {dishId=dishId} );
    }
    //Delete
    [HttpPost("dishes/{dishId}/delete")]
    public IActionResult DeleteDish (int dishId)
    {
        Dish? ToBeDeleted = _context.Dishes.SingleOrDefault(d=>d.DishId == dishId);
        if(ToBeDeleted != null)
        {
            _context.Remove(ToBeDeleted);
            _context.SaveChanges();
        }
        return RedirectToAction ("AllDishes");
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
