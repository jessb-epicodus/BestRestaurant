using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BestRestaurant.Models;

namespace BestRestaurant.Controllers {
  public class CuisinesController : Controller {
    private readonly BestRestaurantContext _db;
    public CuisinesController(BestRestaurantContext db) {
      _db = db;
    }
    public ActionResult Index() {
      List<Cuisine> model = _db.Cuisines.ToList();
      ViewBag.PageTitle = "View All Cuisines";
      return View(model);
    }
    public ActionResult Create() {
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Type");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Cuisine cuisine) {
      _db.Cuisines.Add(cuisine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // GET request-find a specific restaurant and then pass it to the view
    public ActionResult Details(int id) {
      Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      
      return View(thisCuisine);
    }
    // GET 
    public ActionResult Edit(int id) {
      var thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Type");
      return View(thisCuisine);
    }
    [HttpPost]
    public ActionResult Edit(Cuisine cuisine) {
      _db.Entry(cuisine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    //GET
    public ActionResult Delete(int id) {
      var thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      return View(thisCuisine);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id) {
      var thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      _db.Cuisines.Remove(thisCuisine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}