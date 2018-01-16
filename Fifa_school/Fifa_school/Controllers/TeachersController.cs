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
    public class TeachersController : Controller
    {
        private ApplicationDbcontext db = new ApplicationDbcontext();
        UserCredentials credentials = new UserCredentials();
        // GET: Teachers
        public ActionResult Index()
        {

            int userStatus = 0;
            try
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                    {
                        userStatus = credentials.IsUserLoginwithRole("Admin", (Users)Session["User"]);      //Check for Admin
                    }
                    else
                    {
                        userStatus = credentials.IsUserLoginwithRole("Teacher", (Users)Session["User"]);        //Check for Teacher
                    }

                    if (userStatus == 1)                                    //Role Matched
                    {
                        //ViewBag.index = "our first View Bag";
                        Session["index"] = "our first Session";
                        var teacher = db.Teacher.Include(t => t.Branch);
                        return View(teacher.ToList());
                    }                 
                      
                    
                }
                if (userStatus == 0)            //RoleNot Matched
                {
                    return RedirectToAction("Login", "Users", null);
                }
                else
                {
                    return HttpNotFound();
                }

            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            string[] roles = new string[2];
            roles[0] = "Admin";
            roles[1] = "Teacher";
            int userstatus = credentials.IsUserLoginwithMultipleRole(roles, (Users)Session["User"]);
            if (userstatus == 1)
            {
                Session["index"] = null;
                var getSession = Session["index"];
                ViewBag.Branch_id = new SelectList(db.Branch, "Branch_id", "Branch_name");
                return View();
            }
            else 
            {
                return RedirectToAction("Login", "Users", null);
            }
          
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Teacher_id,Teacher_Name,Teacher_FatherName,Dateofjoin,Branch_id")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teacher.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Branch_id = new SelectList(db.Branch, "Branch_id", "Branch_name", teacher.Branch_id);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.Branch_id = new SelectList(db.Branch, "Branch_id", "Branch_name", teacher.Branch_id);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Teacher_id,Teacher_Name,Teacher_FatherName,Dateofjoin,Branch_id")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Branch_id = new SelectList(db.Branch, "Branch_id", "Branch_name", teacher.Branch_id);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teacher.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teacher.Find(id);
            db.Teacher.Remove(teacher);
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
