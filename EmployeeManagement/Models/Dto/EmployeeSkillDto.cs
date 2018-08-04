using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Models.Dto
{
    public class EmployeeSkillDto
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int SkillId { get; set; }

        public int YearsExperience { get; set; }

        public List<EmployeeSkill> Employee { get; set; }

        
    }
}