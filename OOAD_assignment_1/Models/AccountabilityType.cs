using System.ComponentModel.DataAnnotations;

namespace OOAD_assignment_1.Models
{
    public class AccountabilityType
    {
        public int AccountabilityTypeId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
