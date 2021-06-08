using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = "";

        [Required]
        [Range(18,55,ErrorMessage ="Currently, We have no positions vacant for your Age")]
        [DisplayName("Age in Years")]
        public string Age { get; set; } 

        [Required]
        [StringLength(50)]
        public string Qualification { get; set; } = "";


        [Required]
        [Range(1, 25, ErrorMessage = "Currently, We have no positions vacant for your Experience")]
        [DisplayName("Total Experience in Years")]
        public string TotalExperience { get; set; }
        public virtual List<Experience> Experiences { get; set; } = new List<Experience>();


        public string PhotoUrl { get; set; }

        [Required(ErrorMessage ="Please choose the profile photo")]
        [Display(Name ="Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

    }
}
