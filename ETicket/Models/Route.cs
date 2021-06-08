using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ETicket.Models
{
    public class Route
    {
        [Key]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Code  must be 3 character")]
        public string Code { get; set; }


        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
    }
}
