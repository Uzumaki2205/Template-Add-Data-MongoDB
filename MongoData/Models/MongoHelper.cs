using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoData.Models
{
    public class MongoHelper
    {
        public static IMongoClient client { get; set; }
        public static IMongoDatabase database { get; set; }
        public static string MongoConnection = "mongodb://admin_User:101220@clustertest-shard-00-00.ovtix.mongodb.net:27017,clustertest-shard-00-01.ovtix.mongodb.net:27017,clustertest-shard-00-02.ovtix.mongodb.net:27017/<dbname>?ssl=true&replicaSet=atlas-6cs3uw-shard-0&authSource=admin&retryWrites=true&w=majority";
        public static string MongoDatabase = "WebStore";
        public static IMongoCollection<Users> users_collection { get; set; }
        public static void ConnectToMongoService()
        {
            try
            {
                client = new MongoClient(MongoConnection);
                database = client.GetDatabase(MongoDatabase);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Random random = new Random();
        public static object GenerateRandomId(int v)
        {
            string str = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(str, v).
                Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}