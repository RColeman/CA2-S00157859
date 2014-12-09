using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Diagnostics;
using System.Web.Mvc;
using System.Data.Entity;
using CA2.Models;

namespace CA2.Controllers
{
    public class HomeController : Controller
    {


        MovieDb db = new MovieDb();   //Datbase Connection
        //
        // GET: /Home/
        public ActionResult Index(string sortOrder)
        {

            ViewBag.PageTitle = "List of Movies";
            ViewBag.numOrder = String.IsNullOrEmpty(sortOrder)?"descNumber":"";
            ViewBag.dateOrder= sortOrder == "ascDate" ? "descDate" : "ascDate";

            IQueryable<Movie> Movies = db.Movies;
            switch (sortOrder)
            {
                case "descDate":
                    Movies = Movies.OrderByDescending(c => c.StartDate).Include("Actors");
                    break;
                case "descNumber":
                    Movies = Movies.OrderByDescending(c => c.Actors.Count).Include("Actors");
                    break;
                case "ascDate":
                    Movies = Movies.OrderBy(c => c.StartDate).Include(c => c.Actors);
                    break;
                default:
                    Movies = Movies.OrderBy(c => c.Actors.Count).Include("Actors");
                    break;
            }
            return View(Movies.ToList());
        }  
        //
        // GET: /Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var q = db.Movies.Find(id); 
            if (q == null)  // find record?
            {
                Debug.WriteLine("Record not found");
                ViewBag.PageTitle = String.Format("Sorry, record {0} not found.", id);
                //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else ViewBag.PageTitle = "Details of " + q.Title + " (" + ((q.Actors.Count==0)?"None":q.Actors.Count.ToString()) + ')';
            return View(q);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Movies, "MovieId", "Title");
            return View();
        }

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "Title", movie.MovieId);
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "Name", movie.Actors);
            return View(movie);
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            Movie movie = db.Movies.Find(id);
            ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "Title", movie.MovieId);
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "Title", movie.Actors);
            return View(movie);
        }

        
         //POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "Name", movie.MovieId);
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "Name", movie.Actors);
            return View(movie);
        }

        
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
    }
