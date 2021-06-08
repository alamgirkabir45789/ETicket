using ETicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.Repositories
{
   public interface IAgentRepository
    {
        Agent GetAgent(int Id);
        IEnumerable<Agent> GetAll();

        Agent Add(Agent agent);
        Agent Update(Agent agent);
        Agent Delete(int Id);
    }
}
