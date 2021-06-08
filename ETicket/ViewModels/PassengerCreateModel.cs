using ETicket.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.ViewModels
{
    public class PassengerCreateModel
    {
        public Passenger Passenger { get; set; }
        public IEnumerable<SelectListItem> Routes { get; set; }
        public IEnumerable<SelectListItem> RouteTos { get; set; }
    }
}
