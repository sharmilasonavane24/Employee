using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModel
{
    public class SkillDto
    {
        public int Id { get; set; }

        public int YearExprience { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}