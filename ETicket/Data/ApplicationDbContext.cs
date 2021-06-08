using ETicket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicket.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteTo> RouteTos { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Agent> Agents{ get; set; }
        public DbSet<Applicant> Applicants{ get; set; }
        public DbSet<Experience> Experiences{ get; set; }
       // public DbSet<BusAgent> BusAgents{ get; set; }
  
    }    
}
