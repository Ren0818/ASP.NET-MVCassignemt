using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ass3.Models;

namespace ass3.Controllers
{
    public class carsController : Controller
    {
        private ass3dbEntities1 db = new ass3dbEntities1();

        // GET: /cars/
        public ActionResult Index(string searchBy, string search)
        {
            if(searchBy == "CarMode")
            {
                return View(db.cars.Where(x => x.CarMode.StartsWith(search) || search == null).OrderBy(p => p.CarMode).ToList());
            }
        
            var cars = db.cars.Include(c => c.companies);
           
            return View(cars.OrderBy(p =>p.CarMode).ToList());
        }

        // GET: /cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cars cars = db.cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // GET: /cars/Create
        public ActionResult Create()
        {
            ViewBag.Company = new SelectList(db.companies, "Id", "Company");
            return View();
        }

        // POST: /cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include="Id,CarMode,Company,Price")] cars cars)
        {
            if (ModelState.IsValid)
            {
                db.cars.Add(cars);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Company = new SelectList(db.companies, "Id", "Company", cars.Company);
            return View(cars);
        }

        // GET: /cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cars cars = db.cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company = new SelectList(db.companies, "Id", "Company", cars.Company);
            return View(cars);
        }

        // POST: /cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Edit([Bind(Include="Id,CarMode,Company,Price")] cars cars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cars).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Company = new SelectList(db.companies, "Id", "Company", cars.Company);
            return View(cars);
        }

        // GET: /cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cars cars = db.cars.Find(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: /cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cars cars = db.cars.Find(id);
            db.cars.Remove(cars);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
