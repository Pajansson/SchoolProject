using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
