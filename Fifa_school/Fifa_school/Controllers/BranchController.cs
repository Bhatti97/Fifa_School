using Fifa_school.ManagingUsers;
using Fifa_school.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Fifa_school.Controllers
{
    public class BranchController : Controller
    {
        ApplicationDbcontext db = new ApplicationDbcontext();
        UserCredentials credentials = new UserCredentials();
        // GET: Branch
        public ActionResult Index()
        {
            try
            {
                int userStatus = credentials.IsUserLoginwithRole("Admin", (Users)Session["User"]);
                if (userStatus==1)
                {
                    var branch = db.Branch.ToList();
                    return View(branch);
                }
                else if (userStatus == 2)
                {
                    return HttpNotFound();
                }

            }
            catch (Exception)
            {
                
            }            
            return RedirectToAction("Login", "Users", null);
        }
        public ActionResult Create()
        {
            try
            {
                int userStatus = credentials.IsUserLoginwithRole("Admin", (Users)Session["User"]);
                if (userStatus == 1)
                {
                    return View();
                }
                else if (userStatus == 2)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception)
            {
                
            }
            return RedirectToAction("Login", "Users", null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Branch branch_obj)
        {
           
                db.Branch.Add(branch_obj);
                db.SaveChanges();
                return RedirectToAction("Index");
           
        }
        public ActionResult Edit(int? id)
        {
            try
            {
                int userStatus = credentials.IsUserLoginwithRole("Admin", (Users)Session["User"]);
                if (userStatus == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    else
                    {
                        var branch = db.Branch.Find(id);
                        if (branch == null)
                        {
                            return HttpNotFound();
                        }
                        else
                        {
                            return View(branch);
                        }
                    }
                }
                else if (userStatus == 2)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception)
            {

            }
            return RedirectToAction("Login", "Users", null);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Branch branch_obj)
        {
            db.Entry(branch_obj).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            try
            {
                int userStatus = credentials.IsUserLoginwithRole("Admin", (Users)Session["User"]);
                if (userStatus == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    else
                    {
                        var branch = db.Branch.Find(id);
                        if (branch == null)
                        {
                            return HttpNotFound();
                        }
                        else
                        {
                            db.Entry(branch).State = EntityState.Deleted;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else if (userStatus == 2)
                {
                    return HttpNotFound();
                }
            }            
            catch (Exception)
            {

            }
            return RedirectToAction("Login", "Users", null);
    }
    }
}