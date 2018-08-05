using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModel
{
    public class SkillDto
    {
        public int Id { get; set; }

        public int YearExprience { get; set; }
      
        public string Name { get; set; }
      
        public string Description { get; set; }
    }
}