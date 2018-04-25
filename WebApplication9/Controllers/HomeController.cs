using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;
using System.Data;
using System.IO;
using System.Data.Entity;
using System.Net;
using PagedList;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        private RTableEntities1 db = new RTableEntities1();
        // GET: Home
        public ActionResult Index()
        {            
            return View(db.Recipes.ToList());
        }


        public ViewResult List(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CuisineSortParm = String.IsNullOrEmpty(sortOrder) ? "cuisine_desc" : "";
            var recipe = from r in db.Recipes select r;

            if (searchString != null)
            {
                page = 1;
            }
            else 
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                recipe = recipe.Where(r => r.Cuisine.Contains(searchString));
                                    //|| r.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "cuisine_desc":
                    recipe = recipe.OrderByDescending(r => r.Cuisine);
                    break;

                default:
                    recipe = recipe.OrderBy(r => r.Cuisine);
                    break;
            } 

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(recipe.ToPagedList(pageNumber, pageSize));
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if(recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }


        // GET: Home/Create
        
        public ActionResult Create()
        {
            var recipe = new Recipe();
            recipe.CreateIngrediants();
            return View(recipe);
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult NewCreate(Recipe recipe)
        {
            RTableEntities1 db = new RTableEntities1();
      
                if (ModelState.IsValid)
                {
                    foreach (Ingrediant ings in recipe.Ingrediants.ToList())
                    {

                        if (ings.DeleteIng == true)
                        {
                            //Delete ingrediants which is marked to remove
                            recipe.Ingrediants.Remove(ings);
                        }
                    }
                db.Recipes.Add(recipe);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            Recipe recipe = db.Recipes.Find(id);
            if(recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Recipe recipe)
        {
           if (ModelState.IsValid)
           {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();

                foreach (var item in recipe.Ingrediants)
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
           }
            return View(recipe);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            if(recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            var ings = db.Ingrediants.Where(i => i.RecipeId == recipe.RecipeId);
            DeleteChilds(ings);
            db.Recipes.Remove(recipe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void DeleteChilds(IEnumerable<Ingrediant> ings)
        {
            foreach ( var item in ings )
            {
                db.Ingrediants.Remove(item);
            }
        }
    }
}
