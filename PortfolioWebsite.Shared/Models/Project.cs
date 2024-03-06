using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortfolioWebsite.Shared.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID;
        public string Title { get; set; }
        public List<string> Highlights { get; set; }
        public List<string> ImageUrls { get; set; }
        public string GithubURL { get; set; }
        public string ProjectURL { get; set; }
        public string Description { get; set; }
        public string SubTitle { get; set; }
    }
}
