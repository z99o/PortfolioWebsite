using PortfolioWebsite.Shared.Models;
using PortfolioWebsite.Shared.Repository.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioWebsite.Shared.Connection;

namespace PortfolioWebsite.DataAccess
{
    public class ProjectRepository : IRepository<Project, string>
    {

        private IMongoDatabase _db;
        public IMongoCollection<Project> Records { get
            {
                return _db.GetCollection<Project>("Projects");
            }
        }
        public ProjectRepository()
        {
            this._db = DatabaseConnection.GetDatabase();
        }

        public async Task<Project> Create(Project entity)
        {
            entity.ID = "";
			await Records.InsertOneAsync(entity);
			return entity;
		}

        public async Task<string> Delete(string id)
        {
            var proj = await GetByID(id);
            if (proj.ImageUrls != null)
            {
                proj.ImageUrls.ForEach(x=>
                {
					var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", x.Split('/')[2]);
					if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                });
            }
			await Records.DeleteOneAsync(x => x.ID == id);
            return id;
        }
        public async Task<List<Project>> GetAll()
        {
			return await Records.Find(x => true).ToListAsync();
		}

        public async Task<Project> GetByID(string id)
        {
			return await Records.Find(x => x.ID == id).FirstOrDefaultAsync();
		}

        public async Task<Project> Update(Project entity)
        {
			await Records.ReplaceOneAsync(x => x.ID == entity.ID, entity);
			return entity;
		}
    }
}
