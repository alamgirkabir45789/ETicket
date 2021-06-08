using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.Models
{
    public class Experience
    {
        public Experience()
        {

        }
        [Key]
        public int ExperienceId { get; set; }

        [ForeignKey("Applicant")]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get;private set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        [Required]
        [Range(1,25,ErrorMessage ="Years must between 1 and 25")]
        public int YearsWorked { get; set; }

    }
}
