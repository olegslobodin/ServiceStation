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
    public class OrdersController : Controller
    {
        private ServiceStationEntities db = new ServiceStationEntities();
        
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        
        public ActionResult Create(long? clientId, long? carId)
        {

            if (carId == null)
            {
                ViewBag.CarId = clientId == null ? new SelectList(db.Cars, "id", "VIN") :
                    new SelectList(db.Cars.Where(c => c.ClientId == clientId), "id", "VIN");
                ViewBag.ClientId = clientId;
            }
            else
            {
                ViewBag.CarSelected = true;
                ViewBag.CarId = carId;
            }
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "id", "Status");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CarId,Date,Amount,StatusId")] Order order)
        {
            if (TempData["CarId"] != null)
                order.CarId = (long)TempData["CarId"];

            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Details", "Cars", new { id = order.CarId });
            }

            ViewBag.CarId = new SelectList(db.Cars, "id", "VIN", order.CarId);
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "id", "Status", order.StatusId);
            return View(order);
        }
        
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "id", "VIN", order.CarId);
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "id", "Status", order.StatusId);
            return View(order);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CarId,Date,Amount,StatusId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Cars", new { id = order.CarId });
            }
            ViewBag.CarId = new SelectList(db.Cars, "id", "VIN", order.CarId);
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "id", "Status", order.StatusId);
            return View(order);
        }
        
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Details", "Cars", new { id = order.CarId });
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
