using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement;
using EmployeeManagement.ViewModel;

namespace EmployeeManagement.Controllers
{
    public class SkillsController : Controller
    {
        private EmployeeDataModel db = new EmployeeDataModel();

        // GET: Skills
        public ActionResult Index()
        {
            List<SkillDto> skillDtos=new List<SkillDto>();
            foreach (var dbskill in db.Skills.ToList())
            {
                var des = "";
                if (dbskill.Description != null)
                {
                     des = (dbskill.Description).Length >= 100
                        ? dbskill.Description.Substring(0, 100) + "..."
                        : dbskill.Description;
                }
              
                skillDtos.Add(new SkillDto()
                {
                    Description =des ,
                    Id = dbskill.Id,
                    Name = dbskill.Name
                });
            }
            return View(skillDtos);
        }

        // GET: Skills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            var skillDto=new SkillDto()
            {
                Description = skill.Description,Id=skill.Id,Name = skill.Name
            };


            return View(skillDto);
        }

        // GET: Skills/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] SkillDto skillDto)
        {
            if (ModelState.IsValid)
            {

                db.Skills.Add(new Skill()
                {
                    Description = skillDto.Description,Name = skillDto.Name
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skillDto);
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            var skillDto = new SkillDto()
            {
                Description = skill.Description,
                Id = skill.Id,
                Name = skill.Name
            };
            return View(skillDto);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] SkillDto skill)
        {
            if (ModelState.IsValid)
            {
                var dbSkill= db.Skills.Find(skill.Id);

                if (dbSkill != null)
                {
                    dbSkill.Name = skill.Name;
                    dbSkill.Description = skill.Description;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skill);
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
