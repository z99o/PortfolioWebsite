using PortfolioWebsite.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioWebsite.DataAccess
{
    public class ProjectDataAccessLayer
    {
        ProjectDBContext db = new ProjectDBContext();

        //To Get all projects details
        public IEnumerable<Project> GetAllProjects()
        {
            try
            {
                return db.ProjectRecords.Find(_ => true).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
