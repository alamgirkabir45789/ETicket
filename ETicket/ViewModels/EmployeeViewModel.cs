using ETicket.Attirbutes.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.ViewModels
{
    public class EmployeeViewModel:EditImageViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string EmployeeName { get; set; }

        [Required]
        [StringLength(100)]
        public string Designation { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Phone number within 10-11 Character")]
        public string ContactNo { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Joining Date")]
        [Today(ErrorMessage = "Date must be Today")]
        public DateTime JoiningDate { get; set; }


        


    }
}
