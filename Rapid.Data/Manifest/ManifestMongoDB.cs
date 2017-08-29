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
using System.Configuration;

namespace Rapid.Data.Manifest
{
    public class ManifestMongoDB : IManifest
    {
        private static MongoClient _client = null;
        private static IMongoDatabase _data = null;
        private const string MANIFEST_COLLECTION = "manifest";
        public ManifestMongoDB()
        {
            if (_client == null)
            {
                initDatabaseClient();
            }
        }
        private void initDatabaseClient()
        {
            try
            {
                string _dbConnectionString = ConfigurationManager.AppSettings["MANIFEST_DB_CONN"];
                string _dbDatabase = ConfigurationManager.AppSettings["MANIFEST_DB_NAME"];

                _client = new MongoDB.Driver.MongoClient(_dbConnectionString);
                _data = _client.GetDatabase(_dbDatabase);
            }
            catch (Exception ex)
            {
                _client = null;
            }
        }
        public dynamic Create(dynamic manifest)
        {
            if (manifest._id == null)
            {
                manifest._id = Guid.NewGuid().ToString();
            }

            var _manifestCollection = _data.GetCollection<BsonDocument>(MANIFEST_COLLECTION);

            return _manifestCollection.InsertOne(manifest.ToBsonDocument());
            //throw new NotImplementedException();
        }

        public List<dynamic> Search(dynamic searchQuery)
        {
            List<dynamic> _list = new List<dynamic>();

            var _manifestCollection = _data.GetCollection<BsonDocument>(MANIFEST_COLLECTION);
            var result = _manifestCollection.Find(new BsonDocument())
                //.Project(Builders<BsonDocument>.Projection.Exclude("_id"))  
                .ToList();
            foreach (var manifest in result)
            {
                _list.Add(ToDynamic(manifest));
            }
            return _list;
        }

        public dynamic Save(dynamic manifest)
        {
            var _manifestCollection = _data.GetCollection<BsonDocument>(MANIFEST_COLLECTION);
            ReplaceOneResult result = _manifestCollection.ReplaceOne(Builders<BsonDocument>.Filter.Eq("_id", manifest._id), manifest.ToBsonDocument());
            return result.MatchedCount > 0;
        }

        public dynamic Remove(dynamic manifest)
        {
            var _manifestCollection = _data.GetCollection<BsonDocument>(MANIFEST_COLLECTION);
            DeleteResult result = _manifestCollection.DeleteOne(
                Builders<BsonDocument>.Filter.Eq("_id", manifest._id)
                , null);
            return result.DeletedCount > 0;
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

        public List<dynamic> FilterStandard(string AirwayBill, string BoxID, string FlightNo, string Shipmment, DateTime TimeCreatedFrom, DateTime TimeCreatedTo, bool IsTranslated, bool IsApproved)
        {
            List<dynamic> _list = new List<dynamic>();
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Ne("_id",BsonNull.Value );
            if (!String.IsNullOrEmpty(AirwayBill))
                filter = filter & builder.Eq("item.MasterAirWayBill", AirwayBill);
            if (!String.IsNullOrEmpty(BoxID))
                filter = filter & builder.Eq("item.BoxID", BoxID);
            if (!String.IsNullOrEmpty(FlightNo))
                filter = filter & builder.Eq("item.FlightNumber", FlightNo);

            var _manifestCollection = _data.GetCollection<BsonDocument>(MANIFEST_COLLECTION);
            var result = _manifestCollection.Find(new BsonDocument())
                //.Project(Builders<BsonDocument>.Projection.Exclude("_id"))  
                .ToList();
            foreach (var manifest in result)
            {
                _list.Add(ToDynamic(manifest));
            }
            return _list;
        }
    }
}
