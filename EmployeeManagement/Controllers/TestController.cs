using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class TestController : Controller
    {
        private EmployeeDataModel db = new EmployeeDataModel();

        // GET: Test
        public ActionResult Index()
        {
            ViewBag.SkillName = new SelectList(db.Skills, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDto employeeDto)
        {
            return null;
        }

    }
}