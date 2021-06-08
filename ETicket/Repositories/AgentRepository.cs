using ETicket.Data;
using ETicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly ApplicationDbContext db;

        public AgentRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Agent Add(Agent agent)
        {
            db.Agents.Add(agent);
            db.SaveChanges();
            return agent;
        }

        public Agent Delete(int Id)
        {
            Agent agent = db.Agents.Find(Id);
            if (agent != null)
            {
                db.Agents.Remove(agent);
                db.SaveChanges();
            }
            return agent;
        }

        public Agent GetAgent(int Id)
        {
            return db.Agents.Where(x => x.Id == Id).SingleOrDefault();

        }

        public IEnumerable<Agent> GetAll()
        {
            return db.Agents;

        }

        public Agent Update(Agent agent)
        {
            db.Entry(agent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return agent;
        }
    }
}
