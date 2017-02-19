using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using AzureRest.Models;
using MongoDB.Driver;
using System.Xml.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MongoDBLayer
{
    public class MongoLayer : IMongoLayer
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        private string _collectionName;
        private string _databaseName;
        public MongoLayer(string collectionName,string dbName)
        {
            _collectionName = collectionName;
            _databaseName = dbName;
        }


        public IMongoClient MongoClient
        {
            get
            {
                if(_client == null)
                {
                    _client = new MongoClient();
                }
                return _client;
            }
        }

        public IMongoDatabase MongoDatabase
        {
            get
            {
                if (_database == null)
                {
                    _database = MongoClient.GetDatabase(_databaseName);
                }
                return _database;
            }
        }


        public void Add(DeviceData data)
        {
            try
            {
                // _database = _client.GetDatabase("test");
                var collection = MongoDatabase.GetCollection<BsonDocument>(_collectionName);
                var document = new BsonDocument
                 {
                   { "Device" , new BsonDocument
                     {
                       { "DeviceID", data.DeviceID.ToString() },
                       { "ValueA", data.ValueA},
                       { "ValueB", data.ValueB },
                       { "ValueC", data.ValueC },
                       { "ValueD", data.ValueD },
                       { "CreatedDateTime", data.CreatedDateTime }
                     }
                   }
                };
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DeviceData> GetData(string deviceId)
        {
            List<DeviceData> dData = new List<DeviceData>();
           
            var collection = MongoDatabase.GetCollection<BsonDocument>(_collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("Device.DeviceID", deviceId);
            var list = collection.Find(filter).ToList();
            foreach (var dc in list)
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                };
                DeviceData results = JsonConvert.DeserializeObject<DeviceData>(dc["Device"].ToString(), microsoftDateFormatSettings);
                // data.Add(dc[0].ToString());
                //dData.Add(new DeviceData {DeviceID=Convert.ToInt32(dc["DeviceID"]),ValueA= Convert.ToInt32(dc["ValueA"]),ValueB= Convert.ToInt32(dc["ValueB"]),ValueC= Convert.ToInt32(dc["ValueC"]),ValueD= Convert.ToInt32(dc["ValueD"]) });
                dData.Add(results);
            }
            return dData;
        }


        public List<string> GetDeviceList(string customerId)
        {
            List<string> dData = new List<string>();

            var collection = MongoDatabase.GetCollection<BsonDocument>(_collectionName);
            //this is scrap code
            var filter = Builders<BsonDocument>.Filter.Ne("Device.DeviceID", 0);
            var list = collection.Find(filter).ToList();
            foreach (var dc in list)
            {
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                };
                DeviceData results = JsonConvert.DeserializeObject<DeviceData>(dc["Device"].ToString(), microsoftDateFormatSettings);
                // data.Add(dc[0].ToString());
                //dData.Add(new DeviceData {DeviceID=Convert.ToInt32(dc["DeviceID"]),ValueA= Convert.ToInt32(dc["ValueA"]),ValueB= Convert.ToInt32(dc["ValueB"]),ValueC= Convert.ToInt32(dc["ValueC"]),ValueD= Convert.ToInt32(dc["ValueD"]) });
                if (!dData.Contains(results.DeviceID.ToString()))
                {
                    dData.Add(results.DeviceID.ToString());
                }
            }
            return dData;
        }


    }
}
