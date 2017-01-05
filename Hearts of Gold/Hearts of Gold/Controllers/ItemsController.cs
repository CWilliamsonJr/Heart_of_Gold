using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hearts_of_Gold.Models;
using Hearts_of_Gold.ViewModels;
using Microsoft.AspNet.Identity;

namespace Hearts_of_Gold.Controllers
{
    [Authorize]
    public class ItemsController : Controller
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

        // GET: Items
        [AllowAnonymous]
        public ActionResult Index()
        {
            var userId = ReturnUserId();

            var items = db.Items.Include(i => i.Donation_Categories).Include(i => i.Donation_Location).Include(i => i.User).ToList();
            var requests = db.Requests.Where(r => r.RequesterID == userId).ToList();
            var tuple = new Tuple<List<Item>,List<Request>>(items,requests); // combines the item and request into one object.
            
            return View(tuple);
        }

        public ActionResult MyItems()
        {
            ViewBag.userID = HttpContext.User.Identity.GetUserId();
            var userId = HttpContext.User.Identity.GetUserId();
            var items =
                db.Items.Include(i => i.Donation_Categories)
                    .Include(i => i.Donation_Location)
                    .Include(i => i.User)
                    .Where(i => i.User.AspNetUser.Id == userId);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            ViewBag.userID = HttpContext.User.Identity.GetUserId();

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
        public ActionResult Create([Bind(Include = "CategoryID,LocationID,Item1,Quantity,Description")] Item item)
        {
            if (ModelState.IsValid)
            {
                var id = ReturnUserId();
                if (id == 0) return RedirectToAction("Create", "Users"); // if user doesn't exist take the to user creating page

                item.UserID = id; // adds user Id to item object
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
        public ActionResult Edit([Bind(Include = "ItemID,CategoryID,LocationID,Item1,Quantity,Description")] Item item)
        {
            var userId = ReturnUserId();
            var userCompareId = db.Items.Where(i => i.ItemID == item.ItemID).Select(i => i.UserID).FirstOrDefault();
            var itemId = db.Items.Where(i => i.ItemID == item.ItemID).Select(i => i.ItemID).FirstOrDefault();

            if (userId != userCompareId || itemId != item.ItemID)
            {
                ViewData["error"] = "You can only edit your own item";
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                item.UserID = userId; // assigns userid to the item.
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
            var userId = ReturnUserId();
            var userCompareId = db.Items.Where(i => i.ItemID == item.ItemID).Select(i => i.UserID).FirstOrDefault();
            var itemId = db.Items.Where(i => i.ItemID == item.ItemID).Select(i => i.ItemID).FirstOrDefault();

            if (userId != userCompareId || itemId != item.ItemID)
            {
                ViewData["error"] = "You can only delete your own item";
                return View("Error");
            }
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
