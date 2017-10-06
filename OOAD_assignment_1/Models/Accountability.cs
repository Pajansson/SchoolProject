using System.ComponentModel.DataAnnotations;

namespace OOAD_assignment_1.Models
{
    public class Accountability
    {
        public int CommissionerId { get; set; }
        public int AccountableId { get; set; }
        public int AccountabilityTypeId { get; set; }
        [Required]
        public virtual Party Commissioner { get; set; }
        [Required]
        public virtual Party Accountable { get; set; }
        [Required]
        public virtual AccountabilityType AccountabilityType { get; set; }
    }
}
