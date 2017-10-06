using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OOAD_assignment_1.Models.AccountabilityTypesViewModels
{
    public class DeleteViewModel
    {
        public AccountabilityType AccountabilityType { get; set; }

        [Display(Name = "Affected parties")]
        public List<Accountability> AffectedAccountabilities { get; set; }

        public bool CanDelete { get; set; }
    }
}
