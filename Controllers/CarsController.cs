using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ServiceStation.Models;

namespace ServiceStation.Controllers
{
    public class CarsController : Controller
    {
        private ServiceStationEntities db = new ServiceStationEntities();

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        public ActionResult Create(long? clientId)
        {
            if (clientId == null)
                ViewBag.ClientId = new SelectList(db.Clients, "id", "FirstName");
            else
            {
                ViewBag.ClientSelected = true;
                ViewBag.ClientId = clientId;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ClientId,Make,Model,Year,VIN")] Car car)
        {
            if (TempData["ClientId"] != null)
            {
                car.ClientId = (long)TempData["ClientId"];

                //if !ModelState.IsValid we'll need this fields
                ViewBag.ClientSelected = true;
                ViewBag.ClientId = car.ClientId;
            }
            else
                ViewBag.ClientId = new SelectList(db.Clients, "id", "FirstName");

            if (!ModelState.IsValid) return View();

            db.Cars.Add(car);
            db.SaveChanges();
            return RedirectToAction("Details", "Clients", new { id = car.ClientId });
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "id", "FirstName", car.ClientId);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ClientId,Make,Model,Year,VIN")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Clients", new { id = car.ClientId });
            }
            ViewBag.ClientId = new SelectList(db.Clients, "id", "FirstName", car.ClientId);
            return View(car);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            var canDelete = car.Orders.Count == 0;
            return View(new Tuple<Car, bool>(car, canDelete));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Car car = db.Cars.Find(id);
            if (car.Orders.Count == 0)
            {
                db.Cars.Remove(car);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "Clients", new { id = car.ClientId });
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
