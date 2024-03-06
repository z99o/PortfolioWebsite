using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortfolioWebsite.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID;
        public string Title { get; set; }
        public string[] Highlights { get; set; }
        public string[] ImageUrls { get; set; }
        public string GithubURL { get; set; }
        public string ProjectURL { get; set; }
        public string Description { get; set; }
    }
}
