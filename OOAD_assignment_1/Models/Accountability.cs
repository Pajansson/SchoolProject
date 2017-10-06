using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OOAD_assignment_1.Models
{
    public class Accountability
    {
        public int CommissionerId { get; set; }
        public int AccountableId { get; set; }
        public int AccountabilityTypeId { get; set; }
        public int? TimePeriodId { get; set; }

        public virtual Party Commissioner { get; set; }
        public virtual Party Accountable { get; set; }
        public virtual AccountabilityType AccountabilityType { get; set; }
        public virtual TimePeriod TimePeriod { get; set; }
    }
}
