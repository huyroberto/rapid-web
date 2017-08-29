using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Dynamic;
using MongoDB.Bson.IO;

namespace Rapid.Data.Manifest
{
    public class ManifestMongoDB : IManifest
    {
        private static MongoClient _client = null;
        private static IMongoDatabase _data = null;
        private const string MANIFEST_COLLECTION = "manifest";
        public ManifestMongoDB()
        {
            if(_client == null)
            {
                initDatabaseClient();    
            }
        }
        private void initDatabaseClient()
        {
            try
            {
                string _dbConnectionString = System.Configuration.ConfigurationSettings.AppSettings["MANIFEST_DB_CONN"];
                string _dbDatabase = System.Configuration.ConfigurationSettings.AppSettings["MANIFEST_DB_NAME"];

                _client = new MongoDB.Driver.MongoClient(_dbConnectionString);
                _data = _client.GetDatabase(_dbDatabase);
            }
            catch(Exception ex)
            {
                _client = null;
            }
        }
        public dynamic Create(dynamic manifest)
        {
            throw new NotImplementedException();
        }

        public List<dynamic> Search(dynamic searchQuery)
        {
            List<dynamic> _list = new List<dynamic>();

            var _manifestCollection = _data.GetCollection<BsonDocument>(MANIFEST_COLLECTION);
            var result = _manifestCollection.Find(new BsonDocument())
                 //.Project(Builders<BsonDocument>.Projection.Exclude("_id"))
                .ToList();
            foreach(var manifest in result)
            {
                _list.Add(ToDynamic(manifest));
            }
            return _list;
        }

        public dynamic Save(dynamic manifest)
        {
            throw new NotImplementedException();
        }

        public dynamic Remove(dynamic manifest)
        {
            throw new NotImplementedException();
        }

        dynamic ToDynamic(BsonDocument bson)
        {
            var json = bson.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict });
            dynamic e = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(json);
            BsonValue id;
            if (bson.TryGetValue("_id", out id))
            {
                // Lets set _id again so that its possible to save document.
                e._id = new ObjectId(id.ToString());
            }
            return e;
        }
    }
}
