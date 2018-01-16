using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fifa_school.Models;
using Fifa_school.ManagingUsers;

namespace Fifa_school.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbcontext db = new ApplicationDbcontext();
        UserCredentials credentials = new UserCredentials();
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        public ActionResult Login()
        {
            try
            {
                if (credentials.IsUserLoginwithRole("Admin",(Users)Session["User"])==1)     //User found
                {
                    return RedirectToAction("Index", "Branch", null);
                }
                else if(credentials.IsUserLoginwithRole("Teacher", (Users)Session["User"]) == 1)                
                {
                    return RedirectToAction("Index", "Teachers", null);
                }                
            }
            catch (Exception)
            {
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users users)
        {
            Users foundUser = db.Users.Where(m => m.UserName == users.UserName && m.Password == users.Password).FirstOrDefault();
            if (foundUser != null)
            {
                //User found
                Session["User"] = foundUser;
                if(foundUser.user_role=="Admin")
                     return RedirectToAction("index", "Branch", null);
                else
                    return RedirectToAction("index", "Teachers", null);
            }
            return View();

        }
        // GET: Users/Register
        public ActionResult Register()
        {
           
            try
            {
                Users user =(Users) Session["User"];
                if (credentials.IsUserLogin(user)) 
                {
                    return RedirectToAction("Index", "Branch", null);
                }
            }
            catch (Exception ex)
            {

            }
            return View();

        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register( Users users)
        {         
            users.user_role = "Teacher";
            users.status = true;
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,UserName,Password,user_role,status")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
