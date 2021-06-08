using ETicket.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.Models
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(70)]
        [DisplayName("Passenger Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(70)]
        public string Age { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [MaxLength(70)]
        public string Gender { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone number must be 11 character")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int PassengerNo { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int Price { get; set; }

        [Display(Name = "Total Fare")]
        [NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Total { get { return (PassengerNo * Price); } }



        [DisplayName(" Deperate Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        [FutureDateAttribute(ErrorMessage = "Date must not less than or equal Today")]
        public DateTime JournyDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName(" Email Address")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(3)]
        [DisplayName("Deperature From")]
        [ForeignKey("Route")]
        public string RouteCode { get; set; }
        public virtual Route Route { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [MaxLength(3)]
        [DisplayName("Deperature To")]
        [ForeignKey("RouteTo")]

        public string RouteToCode { get; set; }
        public virtual RouteTo RouteTo { get; set; }


    }
}
