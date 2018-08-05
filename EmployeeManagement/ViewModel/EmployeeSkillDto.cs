using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModel
{
    public class EmployeeSkillDto
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public List<SkillDto> Skilllist { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(30)]
        public string PhoneNumber { get; set; }


    }

 
}