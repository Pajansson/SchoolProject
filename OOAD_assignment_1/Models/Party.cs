using System.ComponentModel.DataAnnotations;

namespace OOAD_assignment_1.Models
{
    public class Party
    {
        public int PartyId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
