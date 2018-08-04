using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement;
using EmployeeManagement.Models.Dto;

namespace EmployeeManagement.Controllers
{
    public class EmployeeSkillDtoController : Controller
    {
        private EmployeeDataModel db = new EmployeeDataModel();

        // GET: EmployeeSkillDto
        public ActionResult Index()
        {
            var employeeSkillDtoes = db.EmployeeSkillDtoes.Include(e => e.Employee);
            return View(employeeSkillDtoes.ToList());
        }

        // GET: EmployeeSkillDto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkillDto employeeSkillDto = db.EmployeeSkillDtoes.Find(id);
            if (employeeSkillDto == null)
            {
                return HttpNotFound();
            }
            return View(employeeSkillDto);
        }

        // GET: EmployeeSkillDto/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName");
            return View();
        }

        // POST: EmployeeSkillDto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeSkillDto employeeSkillDto)
        {
            if (ModelState.IsValid)
            {
                //db.Employees.Add(employeeSkillDto.Employee);
                //db.SaveChanges();
                //var currentEmployee = db.Employees.FirstOrDefault(
                //    e => e.FirstName == employeeSkillDto.Employee.FirstName &&
                //         e.LastName == employeeSkillDto.Employee.LastName);


                //foreach (var empSkill in employeeSkillDto.Skill)
                //{
                //    db.EmployeeSkills.Add(new EmployeeSkill()
                //    {
                //        Employee = currentEmployee,
                //        Skill = empSkill,
                //        YearsExperience = employeeSkillDto.YearsExperience

                //    });
                //}
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeSkillDto.EmployeeId);
            return View(employeeSkillDto);
        }

        // GET: EmployeeSkillDto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkillDto employeeSkillDto = db.EmployeeSkillDtoes.Find(id);
            if (employeeSkillDto == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeSkillDto.EmployeeId);
            return View(employeeSkillDto);
        }

        // POST: EmployeeSkillDto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,SkillId,YearsExperience")] EmployeeSkillDto employeeSkillDto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeSkillDto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeSkillDto.EmployeeId);
            return View(employeeSkillDto);
        }

        // GET: EmployeeSkillDto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkillDto employeeSkillDto = db.EmployeeSkillDtoes.Find(id);
            if (employeeSkillDto == null)
            {
                return HttpNotFound();
            }
            return View(employeeSkillDto);
        }

        // POST: EmployeeSkillDto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeSkillDto employeeSkillDto = db.EmployeeSkillDtoes.Find(id);
            db.EmployeeSkillDtoes.Remove(employeeSkillDto);
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
