using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOAD_assignment_1.Models;

namespace OOAD_assignment_1.ViewModels
{
    public class PartyDetailsViewModel
    {
        public PartyDetailsViewModel()
        {
            Accountabilities = new List<Accountability>();
        }
        public Party Party { get; set; }
        public Accountability Accountability { get; set; }
        public List<Accountability> Accountabilities { get; set; }
        //public AccountabilityType AccountabilityType { get; set; }
        //public List<AccountabilityType> AccountabilityTypes { get; set; }
    }
}
