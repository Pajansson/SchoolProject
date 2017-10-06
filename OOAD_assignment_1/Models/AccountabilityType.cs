using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_assignment_1.Models
{
    public class AccountabilityType
    {
        public int AccountabilityTypeId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
