using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hearts_of_Gold.Models;
using Microsoft.AspNet.Identity;

namespace Hearts_of_Gold.Controllers
{
    public class RequestsController : Controller
    {
        private Hearts_Of_GoldEntities db = new Hearts_Of_GoldEntities();
        private int ReturnUserId() // Gets the user id from the users table and returns it.
        {
            var aspNetUserId = HttpContext.User.Identity.GetUserId();
            return
                db.Users
                .Where(i => i.AspNetUsersId == aspNetUserId)
                .Select(i => i.UserID).FirstOrDefault();
        }

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
            ViewBag.RequesterID = new SelectList(db.Users, "UserID", "AspNetUsersId");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestID,DonationItemID,LocationID,RequesterID,Quantity,ItemPickedUp")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName", request.LocationID);
            ViewBag.DonationItemID = new SelectList(db.Items, "ItemID", "Item1", request.DonationItemID);
            ViewBag.RequesterID = new SelectList(db.Users, "UserID", "AspNetUsersId", request.RequesterID);
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
            ViewBag.RequesterID = new SelectList(db.Users, "UserID", "AspNetUsersId", request.RequesterID);
            return View(request);
        }

        public ActionResult MakeRequest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Bind(Include = "Quantity,ItemID,")]
        public ActionResult MakeRequest([Bind(Include = "ItemID,Quantity")] Item item)
        {
            var userId = ReturnUserId();
            var dbItem = db.Items.Find(item.ItemID); // gets the requested item from the database.

            if (dbItem != null && userId != 0 && userId != dbItem.UserID) // checks to see if the item exist, the user is valid, and not trying to request their own item.
            {
                var request = new Request()
                {
                    Quantity = item.Quantity, 
                    RequesterID = userId,
                    DonationItemID = dbItem.ItemID,
                    LocationID = dbItem.LocationID
                };
                
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Details", "Items", new { id = item.ItemID });
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestID,DonationItemID,LocationID,RequesterID,Quantity,ItemPickedUp")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName", request.LocationID);
            ViewBag.DonationItemID = new SelectList(db.Items, "ItemID", "Item1", request.DonationItemID);
            ViewBag.RequesterID = new SelectList(db.Users, "UserID", "AspNetUsersId", request.RequesterID);
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
