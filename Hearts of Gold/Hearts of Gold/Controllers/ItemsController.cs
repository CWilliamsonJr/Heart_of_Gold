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
    public class ItemsController : Controller
    {
        private Hearts_Of_GoldEntities db = new Hearts_Of_GoldEntities();

        public IList<T> ReturnItems<T>()
        {
            var items = db.Items.Include(i => i.Donation_Categories)
                        .Include(i => i.Donation_Location)
                        .Include(i => i.User)
                        .Select(i => new ItemViewModels()
                        {
                            Item = i.Item1,
                            Quantity = i.Quantity,
                            Donation_Categories = i.Donation_Categories,
                            Donation_Location = i.Donation_Location,
                            AspNetUsersId = i.User.AspNetUsersId,
                            ItemID = i.ItemID,
                            Description = i.Description
                        }).ToList();
            return (IList<T>) items;
        }

        public IList<T> ReturnItems<T>(string userId)
        {
            var items = db.Items.Include(i => i.Donation_Categories)
                        .Include(i => i.Donation_Location)
                        .Include(i => i.User).Where(i => i.User.AspNetUsersId == userId )
                        .Select(i => new ItemViewModels()
                        {
                            Item = i.Item1,
                            Quantity = i.Quantity,
                            Donation_Categories = i.Donation_Categories,
                            Donation_Location = i.Donation_Location,
                            AspNetUsersId = i.User.AspNetUsersId,
                            ItemID = i.ItemID,
                            Description = i.Description
                        }).ToList();
            return (IList<T>)items;
        }

        // GET: Items
        public ActionResult Index()
        {
            ViewBag.userID = HttpContext.User.Identity.GetUserId(); // used to map the user to their item for edit and delete purposes
            var items = ReturnItems<ItemViewModels>();
            return View(items);
        }

        public ActionResult MyItems()
        {
            ViewBag.userID = HttpContext.User.Identity.GetUserId();
            var items = ReturnItems<ItemViewModels>(ViewBag.userID);
            return View("Index",items);
        }

        // GET: Items/Details/5
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
