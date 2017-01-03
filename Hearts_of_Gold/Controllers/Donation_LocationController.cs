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
    public class Donation_LocationController : Controller
    {
        private Hearts_Of_GoldEntities db = new Hearts_Of_GoldEntities();

        // GET: Donation_Location
        public ActionResult Index()
        {
            return View(db.Donation_Location.ToList());
        }

        // GET: Donation_Location/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation_Location donation_Location = db.Donation_Location.Find(id);
            if (donation_Location == null)
            {
                return HttpNotFound();
            }
            return View(donation_Location);
        }

        // GET: Donation_Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donation_Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,BusinessName,Address")] Donation_Location donation_Location)
        {
            if (ModelState.IsValid)
            {
                db.Donation_Location.Add(donation_Location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donation_Location);
        }

        // GET: Donation_Location/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation_Location donation_Location = db.Donation_Location.Find(id);
            if (donation_Location == null)
            {
                return HttpNotFound();
            }
            return View(donation_Location);
        }

        // POST: Donation_Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,BusinessName,Address")] Donation_Location donation_Location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation_Location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donation_Location);
        }

        // GET: Donation_Location/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation_Location donation_Location = db.Donation_Location.Find(id);
            if (donation_Location == null)
            {
                return HttpNotFound();
            }
            return View(donation_Location);
        }

        // POST: Donation_Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donation_Location donation_Location = db.Donation_Location.Find(id);
            db.Donation_Location.Remove(donation_Location);
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
