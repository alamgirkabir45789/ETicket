using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicket.Models
{
    public class RouteTo
    {
        [Key]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Code  must be 3 character")]
        public string Code { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [ForeignKey("Route")]
        public string RouteCode { get; set; }
        public virtual Route Route { get; set; }
    }
}
