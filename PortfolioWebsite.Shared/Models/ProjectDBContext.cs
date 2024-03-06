using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortfolioWebsite.Models
{
    public class ProjectDBContext
    {

        private readonly IMongoDatabase _db;
        public ProjectDBContext()
        {
            var client = DatabaseConnection.GetClient();
            _db = client.GetDatabase("PortfolioWebsiteDB");
        }

        public IMongoCollection<Project> ProjectRecords
        {
            get
            {
                return _db.GetCollection<Project>("Projects");
            }
        }
    }
}
