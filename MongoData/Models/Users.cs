using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoData.Models
{
    public class Users
    {
        [BsonId]
        public Object Id { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Admin")]
        public bool Roles { get; set; }

        [BsonElement("TimeCreated")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime TimeCreated { get; set; }
    }
}