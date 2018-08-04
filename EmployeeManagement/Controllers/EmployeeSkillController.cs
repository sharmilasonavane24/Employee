using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement;

namespace EmployeeManagement.Controllers
{
    public class EmployeeSkillController : Controller
    {
        private EmployeeDataModel db = new EmployeeDataModel();

        // GET: EmployeeSkill
        public ActionResult Index()
        {
            var employeeSkills = db.EmployeeSkills.Include(e => e.Employee).Include(e => e.Skill);
            return View(employeeSkills.ToList());
        }

        // GET: EmployeeSkill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkill employeeSkill = db.EmployeeSkills.Find(id);
            if (employeeSkill == null)
            {
                return HttpNotFound();
            }
            return View(employeeSkill);
        }

        // GET: EmployeeSkill/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName");
            ViewBag.SkillId = new SelectList(db.Skills, "Id", "Name");
            return View();
        }

        // POST: EmployeeSkill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeSkill employeeSkill, List<Skill> skills)
        {

            if (ModelState.IsValid)
            {
               // db.EmployeeSkills.Add(employeeSkill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

         //   ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeSkill.EmployeeId);
//ViewBag.SkillId = new SelectList(db.Skills, "Id", "Name", employeeSkill.SkillId);
            return View(employeeSkill);
        }

        // GET: EmployeeSkill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkill employeeSkill = db.EmployeeSkills.Find(id);
            if (employeeSkill == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeSkill.EmployeeId);
            ViewBag.SkillId = new SelectList(db.Skills, "Id", "Name", employeeSkill.SkillId);
            return View(employeeSkill);
        }

        // POST: EmployeeSkill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,SkillId,YearsExperience")] EmployeeSkill employeeSkill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeSkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeSkill.EmployeeId);
            ViewBag.SkillId = new SelectList(db.Skills, "Id", "Name", employeeSkill.SkillId);
            return View(employeeSkill);
        }

        // GET: EmployeeSkill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkill employeeSkill = db.EmployeeSkills.Find(id);
            if (employeeSkill == null)
            {
                return HttpNotFound();
            }
            return View(employeeSkill);
        }

        // POST: EmployeeSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeSkill employeeSkill = db.EmployeeSkills.Find(id);
            db.EmployeeSkills.Remove(employeeSkill);
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
