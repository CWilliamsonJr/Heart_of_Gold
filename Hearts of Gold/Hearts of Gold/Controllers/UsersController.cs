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
    public class UsersController : Controller
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

        // GET: Users
        public ActionResult Index()
        {
            //var users = db.Users.Include(u => u.AspNetUser);
            return RedirectToAction("Details");
        }

        // GET: Users/Details/5
        public ActionResult Details()
        {
            var id = ReturnUserId();

            //if (id == 0)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            User user = db.Users.Find(id);
            if (user == null)
            {
                return RedirectToAction("Create");
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            //var userName = HttpContext.User.Identity.Name;
            //ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Firstname,Lastname,Streetaddress,Date_of_Birth")] User user)
        {
            if (ModelState.IsValid)
            {
                user.AspNetUsersId = HttpContext.User.Identity.GetUserId();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", user.AspNetUsersId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit()
        {
            var id = ReturnUserId();
            if (id == 0)
            {
                return RedirectToAction("Create");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", user.AspNetUsersId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Firstname,Lastname,Streetaddress,Date_of_Birth,IsDeleted")] User user)
        {
            var aspNetUsersId = HttpContext.User.Identity.GetUserId();
            var updatedUserId =
                db.Users.Where(u => u.AspNetUsersId == aspNetUsersId).Select(u => u.UserID).FirstOrDefault();

            if (ModelState.IsValid && updatedUserId == user.UserID)
            {
                user.AspNetUsersId = aspNetUsersId;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.AspNetUsersId = new SelectList(db.AspNetUsers, "Id", "Email", user.AspNetUsersId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete()
        {
            var id = ReturnUserId();
            if (id == 0)
            {
                return RedirectToAction("Create");
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed()
        {
            var id = ReturnUserId();
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
