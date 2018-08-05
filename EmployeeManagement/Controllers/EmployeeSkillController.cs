using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.ViewModel;

namespace EmployeeManagement.Controllers
{
    public class EmployeeSkillController : Controller
    {
        private EmployeeDataModel db = new EmployeeDataModel();

        public ActionResult Index()
        {
            var employeeSkillDtoes = new List<EmployeeSkillDto>();
            foreach (var dbEmp in db.Employees)
            {

                employeeSkillDtoes.Add(new EmployeeSkillDto()
                {
                    Id = dbEmp.Id,
                    FirstName = dbEmp.FirstName,
                    LastName = dbEmp.LastName,
                    PhoneNumber = dbEmp.PhoneNumber
                });
            }

            return View(employeeSkillDtoes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkill employeeSkillDto = db.EmployeeSkills.Find(id);
            if (employeeSkillDto == null)
            {
                return HttpNotFound();
            }
            return View(employeeSkillDto);
        }

        public ActionResult Create()
        {
            List<SelectListItem> skills = new List<SelectListItem>();

            foreach (var dbSkill in db.Skills)
            {
                skills.Add(new SelectListItem() { Text = dbSkill.Name, Value = dbSkill.Id.ToString() });
            }
          
            ViewBag.SkillId = new SelectList(skills, "Value", "Text");
            
            ViewModel.EmployeeSkillDto newEmpSkill = new ViewModel.EmployeeSkillDto();
            newEmpSkill.Skilllist = new List<SkillDto>();
            newEmpSkill.Skilllist.Add(new SkillDto());
            return View(newEmpSkill);

        }

        [HttpPost]
        public ActionResult Create(ViewModel.EmployeeSkillDto employeeSkillDto, string submit)
        {
            List<SelectListItem> skills = new List<SelectListItem>();
            foreach (var dbSkill in db.Skills)
            {
                skills.Add(new SelectListItem() { Text = dbSkill.Name, Value = dbSkill.Id.ToString() });
            }

            ViewBag.SkillId = new SelectList(skills, "Value", "Text");
            

            switch (submit)
            {
                case "Cancel":
                    return RedirectToAction("Index");
                case "AddMoreSkill":

                    employeeSkillDto.Skilllist.Add(new SkillDto());

                    return View(employeeSkillDto);
                case "Create":
                    if (ModelState.IsValid)
                    {
                        var newEmployee = new Employee()
                        {
                            FirstName = employeeSkillDto.FirstName,
                            LastName = employeeSkillDto.LastName,
                            PhoneNumber = employeeSkillDto.PhoneNumber
                        };

                        db.Employees.Add(newEmployee);
                        db.SaveChanges();

                        var selectNewEmplyee =
                            db.Employees.FirstOrDefault(e => e.FirstName == employeeSkillDto.FirstName &&
                                                             e.LastName == employeeSkillDto.LastName);

                        foreach (var empSkill in employeeSkillDto.Skilllist)
                        {
                            var newEmpSkill = new EmployeeSkill()
                            {

                                Employee = selectNewEmplyee,
                                EmployeeId = selectNewEmplyee.Id,
                                SkillId = empSkill.Id,
                                YearsExperience = empSkill.YearExprience
                            };
                            db.EmployeeSkills.Add(newEmpSkill);
                            db.SaveChanges();
                        }

                        return RedirectToAction("Index");
                    }

                    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", employeeSkillDto.EmployeeId);
                    return View(employeeSkillDto);
                default:
                    int index = Convert.ToInt32(submit);
                    var removeItem = employeeSkillDto.Skilllist[index];
                    if (employeeSkillDto.Skilllist.Count > 1)
                    {
                        employeeSkillDto.Skilllist.Remove(removeItem);
                    }
                    return View(employeeSkillDto);
            }

            
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
          var  employeeSkillDto =new EmployeeSkillDto()
            {
                FirstName = employee.FirstName,LastName = employee.LastName,PhoneNumber = employee.PhoneNumber
            };
            return View(employeeSkillDto);
        }

        [HttpPost]
         public ActionResult Edit( EmployeeSkillDto employee)
        {
            if (ModelState.IsValid)
            {
                var dbEmp = db.Employees.Find(employee.Id);

                if (dbEmp != null)
                {
                    dbEmp.FirstName = employee.FirstName;
                    dbEmp.LastName = employee.LastName;
                    dbEmp.PhoneNumber = employee.PhoneNumber;

                }


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
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