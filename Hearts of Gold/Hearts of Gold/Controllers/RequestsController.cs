using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hearts_of_Gold.Models;

namespace Hearts_of_Gold.Controllers
{
    public class RequestsController : Controller
    {
        private Hearts_Of_GoldEntities db = new Hearts_Of_GoldEntities();

        // GET: Requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.Donation_Location).Include(r => r.Item).Include(r => r.User);
            return View(requests.ToList());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName");
            ViewBag.DonationItemID = new SelectList(db.Items, "ItemID", "Item1");
            ViewBag.RequesterID = new SelectList(db.Users, "UserID", "Firstname");
            return View();
        }

        // POST: Requests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName", request.LocationID);
            ViewBag.DonationItemID = new SelectList(db.Items, "ItemID", "Item1", request.DonationItemID);
            ViewBag.RequesterID = new SelectList(db.Users, "UserID", "Firstname", request.RequesterID);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName", request.LocationID);
            ViewBag.DonationItemID = new SelectList(db.Items, "ItemID", "Item1", request.DonationItemID);
            ViewBag.RequesterID = new SelectList(db.Users, "UserID", "Firstname", request.RequesterID);
            return View(request);
        }

        // POST: Requests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName", request.LocationID);
            ViewBag.DonationItemID = new SelectList(db.Items, "ItemID", "Item1", request.DonationItemID);
            ViewBag.RequesterID = new SelectList(db.Users, "UserID", "Firstname", request.RequesterID);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
