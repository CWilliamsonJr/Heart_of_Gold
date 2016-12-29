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
    public class ItemsController : Controller
    {
        private Hearts_Of_GoldEntities db = new Hearts_Of_GoldEntities();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Donation_Categories).Include(i => i.Donation_Location).Include(i => i.User);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
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

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Donation_Categories, "CategoryID", "Categories");
            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "AspNetUsersId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,CategoryID,LocationID,UserID,Item1,Quantity,Description")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Donation_Categories, "CategoryID", "Categories", item.CategoryID);
            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName", item.LocationID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "AspNetUsersId", item.UserID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategoryID = new SelectList(db.Donation_Categories, "CategoryID", "Categories", item.CategoryID);
            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName", item.LocationID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "AspNetUsersId", item.UserID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,CategoryID,LocationID,UserID,Item1,Quantity,Description")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Donation_Categories, "CategoryID", "Categories", item.CategoryID);
            ViewBag.LocationID = new SelectList(db.Donation_Location, "LocationID", "BusinessName", item.LocationID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "AspNetUsersId", item.UserID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
